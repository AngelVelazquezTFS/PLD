using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PLD.EF;
using PLD.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLD.Controllers
{
    public class ConfigController : Controller
    {
        DB_Entities db = new DB_Entities();
        public ActionResult Index()
        {
            ViewBag.Title = "Configuración Variables";
            return PartialView();
        }

        #region "RFC Toyota"
        public ActionResult ListaRFC()
        {
            ConfigViewModels modelo = new ConfigViewModels();
            modelo.ListaConfigRFC = ConfigViewModels.getConfigs(Convert.ToInt32(Enums.TipoConfiguracion.RFC));
            return Json(modelo.ListaConfigRFC, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Este método obtiene el RFC y lo imprime en texto de ventana modal 
        /// </summary>
        /// <param name="AML_ID_CVE"></param>
        /// <returns></returns>
        public ActionResult EditRFC(int AML_ID_CVE)
        {
            ConfigViewModels confirmar = new ConfigViewModels();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(AML_ID_CVE.ToString()))
                {
                    confirmar = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == AML_ID_CVE).Select(m => new ConfigViewModels
                    {
                        AML_ID_CVE = AML_ID_CVE,
                        AML_DS_EMAIL = m.AML_DS_EMAIL,
                        AML_DS_DES = m.AML_DS_DES,
                    }).Single();
                }
            }

            string value = string.Empty;
            value = JsonConvert.SerializeObject(confirmar, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Este método actualiza el RFC toyota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveRFC(ConfigViewModels model)
        {
            ResultadoModels R = new ResultadoModels();
            try
            {

                //Obtenemos lo que se actualizó de la tabla usuario
                string valores = string.Empty;
                valores = strCambioRFC(model.AML_ID_CVE, model.AML_DS_EMAIL);
                if (valores == string.Empty)
                    valores = "No se actualizaron datos";


                string userName = User.Identity.GetUserName();
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    db.KCONFIG_AML.ToList()
                        .Where(j => j.AML_ID_CVE == model.AML_ID_CVE).ToList()
                        .ForEach(i =>
                        {
                            i.AML_DS_EMAIL = model.AML_DS_EMAIL;
                            i.AML_FE_MOD = DateTime.Now;
                            i.USR_CL_CVEMOD = userName;

                        });
                    db.SaveChanges();
                }

                BitacoraModels.guardaBitacora(3010, "Se actualizó RFC Toyota", valores, User.Identity.GetUserName());

                R = new ResultadoModels { NotifyMsg = "Registro Actualizado", NotifyType = Enums.eNotify_Type.success, ToURL = Url.Action("ListaRFC", "Config") };

            }
            catch (Exception ex)
            {
                Logs.Log("-->Controlador: Día Método: EliminaDia :: --> EXCEPTION: " + ex.Message.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }
            return PartialView("_Resultado", R);
        }

        private string strCambioRFC(int id, string correo)
        {
            string cambios = string.Empty;

            DialogViewModel confirmar = new DialogViewModel();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    confirmar = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == id).Select(m => new DialogViewModel
                    {
                        Email = m.AML_DS_EMAIL,
                        Descripcion = m.AML_DS_DES,
                    }).Single();
                }
            }

            cambios = "1 RFC Anterior: " + confirmar.Email + ", RFC Actual: " + correo;

            return cambios;
        }

        #endregion

        #region "UMA"

        public ActionResult ListaConfig()
        {
            UMAViewModel modelo = new UMAViewModel();
            modelo.ListUMA = UMAViewModel.getListUMA(Convert.ToInt32(Enums.TipoConfiguracion.UMAS));
            return Json(modelo.ListUMA, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUMA(int AML_ID_CVE)
        {
            UMAViewModel confirmar = new UMAViewModel();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(AML_ID_CVE.ToString()))
                {
                    confirmar = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == AML_ID_CVE).Select(m => new UMAViewModel
                    {
                        AML_ID_CVE = m.AML_ID_CVE,
                        AML_NO_MONTO = (decimal)m.AML_NO_MONTO,
                        AML_DS_DES = m.AML_DS_DES,
                    }).Single();
                }
            }
            string value = string.Empty;
            value = JsonConvert.SerializeObject(confirmar, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUMA(UMAViewModel model)
        {
            ResultadoModels R = new ResultadoModels();
            try
            {
                //Obtenemos lo que se actualizó de la tabla usuario
                string valores = string.Empty;
                valores = strCambioUMA(model.AML_ID_CVE, (decimal)model.AML_NO_MONTO);
                if (valores == string.Empty)
                    valores = "No se actualizaron datos";


                string userName = User.Identity.GetUserName();
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    db.KCONFIG_AML.ToList()
                        .Where(j => j.AML_ID_CVE == model.AML_ID_CVE).ToList()
                        .ForEach(i =>
                        {
                            i.AML_NO_MONTO = model.AML_NO_MONTO;
                            i.AML_FE_MOD = DateTime.Now;
                            i.USR_CL_CVEMOD = userName;

                        });

                    db.SaveChanges();
                }

                BitacoraModels.guardaBitacora(3010, "Se actualizó Valor UMA", valores, User.Identity.GetUserName());
                R = new ResultadoModels { NotifyMsg = "Registro Actualizado", NotifyType = Enums.eNotify_Type.success, ToURL = Url.Action("ListaConfig", "Config")};
            }
            catch (Exception ex)
            {
                Logs.Log("-->Controlador: Día Método: EliminaDia :: --> EXCEPTION: " + ex.Message.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }
            return PartialView("_Resultado", R);
        }

        private string strCambioUMA(int id, decimal UMA)
        {
            string cambios = string.Empty;

            DialogViewModel confirmar = new DialogViewModel();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    confirmar = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == id).Select(m => new DialogViewModel
                    {
                        Email = m.AML_DS_EMAIL,
                        Valor = (decimal)m.AML_NO_MONTO,
                        Descripcion = m.AML_DS_DES,
                    }).Single();
                }
            }

            cambios = id + " UMA Anterior: " + confirmar.Valor + ", UMA Actual: " + UMA;

            return cambios;
        }

        //public ActionResult RegisterConfig(int AML_ID_CVE)
        //{
        //    ConfigViewModels _Config;
        //    using (EF.DB_Entities db = new EF.DB_Entities())
        //    {
        //        if (!string.IsNullOrEmpty(AML_ID_CVE.ToString()))
        //        {
        //            _Config = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == AML_ID_CVE).Select(m => new ConfigViewModels
        //            {
        //                AML_ID_CVE = m.AML_ID_CVE,
        //                AML_DS_DES = m.AML_DS_DES,
        //                AML_NO_MONTO = (decimal)m.AML_NO_MONTO,
        //                AML_FG_STATUS = (int)m.AML_FG_STATUS,
        //                PAR_CL_VALOR = (int)m.PAR_CL_VALOR
        //            }).Single();

        //            ViewBag.TitleRegEdit = "Editar Configuración";
        //        }
        //        else
        //        {
        //            _Config = new ConfigViewModels { AML_ID_CVE = AML_ID_CVE };
        //            ViewBag.TitleRegEdit = "Registrar Configuración";
        //        }
        //    }
        //    return PartialView("RegisterConfig", _Config);
        //}

        //[HttpPost]
        //public ActionResult RegisterConfig(ConfigViewModels model)
        //{
        //    ViewBag.TitleRegEdit = "Registrar Configuración";
        //    string Permisos = string.Empty;

        //    ResultadoModels R = new ResultadoModels();
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (model.AML_ID_CVE == 0)
        //            {
        //                using (EF.DB_Entities db = new EF.DB_Entities())
        //                {
        //                    db.KCONFIG_AML.Add(new EF.KCONFIG_AML()
        //                    {
        //                        PAR_CL_VALOR = (int)Enums.TipoConfiguracion.UMAS,
        //                        AML_DS_DES = model.AML_DS_DES.ToUpper(),
        //                        AML_NO_MONTO = model.AML_NO_MONTO,
        //                        AML_FG_STATUS = model.AML_FG_STATUS,
        //                        USR_CL_CVEALTA = User.Identity.GetUserName(),
        //                        AML_FE_ALTA = DateTime.Now,
        //                    });
        //                    db.SaveChanges();
        //                }
        //                R = new ResultadoModels { NotifyMsg = "Configuración Registrada con éxito", NotifyType = Enums.eNotify_Type.success };
        //            }
        //            else
        //            {
        //                using (EF.DB_Entities db = new EF.DB_Entities())
        //                {
        //                    db.KCONFIG_AML.ToList().Where(l => l.AML_ID_CVE == model.AML_ID_CVE).ToList().ForEach(m =>
        //                    {
        //                        m.AML_DS_DES = model.AML_DS_DES.ToUpper();
        //                        m.AML_NO_MONTO = model.AML_NO_MONTO;
        //                        m.AML_FG_STATUS = model.AML_FG_STATUS;
        //                        m.USR_CL_CVEMOD = User.Identity.GetUserName();
        //                        m.AML_FE_MOD = DateTime.Now;
        //                    });

        //                    db.SaveChanges();
        //                }
        //                R = new ResultadoModels { NotifyMsg = "Configuración Actualizada con éxito", NotifyType = Enums.eNotify_Type.success };
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
        //            R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
        //        }

        //    }
        //    else
        //    {
        //        R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
        //    }
        //    return PartialView("_Resultado", R);
        //}

        #endregion

        #region "CORREOS"
        public ActionResult ListaCorreos()
        {

            ConfigViewModels modelo = new ConfigViewModels();
            modelo.ListaConfigCorreo = ConfigViewModels.getConfigs(Convert.ToInt32(Enums.TipoConfiguracion.CORREOS));
            return Json(modelo.ListaConfigCorreo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCorreo(int AML_ID_CVE)
        {
            ConfigViewModels _Config = new ConfigViewModels();
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(AML_ID_CVE.ToString()))
                {
                    _Config = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == AML_ID_CVE).Select(m => new ConfigViewModels
                    {
                        AML_ID_CVE = m.AML_ID_CVE,
                        AML_DS_DES = m.AML_DS_DES,
                        AML_DS_EMAIL = m.AML_DS_EMAIL,
                        AML_FG_STATUS = (int)m.AML_FG_STATUS,
                    }).Single();
                }
            }
            string value = string.Empty;
            value = JsonConvert.SerializeObject(_Config, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCorreo(ConfigViewModels model)
        {
            ResultadoModels R = new ResultadoModels();
            try
            {

                //Obtenemos lo que se actualizó de la tabla usuario
                string valores = string.Empty;
                valores = strCambioCorreo(model.AML_ID_CVE, model.AML_DS_EMAIL);
                if (valores == string.Empty)
                    valores = "No se actualizaron datos";


                string userName = User.Identity.GetUserName();
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    db.KCONFIG_AML.ToList()
                        .Where(j => j.AML_ID_CVE == model.AML_ID_CVE).ToList()
                        .ForEach(i =>
                        {
                            i.AML_DS_EMAIL = model.AML_DS_EMAIL;
                            i.AML_FE_MOD = DateTime.Now;
                            i.USR_CL_CVEMOD = userName;

                        });
                    db.SaveChanges();
                }

                BitacoraModels.guardaBitacora(3010, "Se actualizó Umbral Alertas", valores, User.Identity.GetUserName());

                R = new ResultadoModels { NotifyMsg = "Registro Actualizado", NotifyType = Enums.eNotify_Type.success, ToURL = Url.Action("ListaRFC", "Config") };

            }
            catch (Exception ex)
            {
                Logs.Log("-->Controlador: Día Método: EliminaDia :: --> EXCEPTION: " + ex.Message.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }
            return PartialView("_Resultado", R);
        }

        private string strCambioCorreo(int id, string correo)
        {
            string cambios = string.Empty;

            DialogViewModel confirmar = new DialogViewModel();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(id.ToString()))
                {
                    confirmar = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == id).Select(m => new DialogViewModel
                    {
                        Email = m.AML_DS_EMAIL,
                        Descripcion = m.AML_DS_DES,
                    }).Single();
                }
            }

            cambios = id + " Correo Anterior: " + confirmar.Email + ", Correo Actual: " + correo;

            return cambios;
        }

        
        #endregion

        #region "PREPAGOS"
        public ActionResult ListaPrepago()
        {
            ConfigViewModels modelo = new ConfigViewModels();
            modelo.ListaConfigPrepago = ConfigViewModels.getConfigs(Convert.ToInt32(Enums.TipoConfiguracion.PREPAGOS));
            return Json(modelo.ListaConfigPrepago, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdPrepago(int Id, int valor, int idcheck)
        {
            ResultadoModels R = new ResultadoModels();
            string valores = Id + " PREPAGO; Activo: " + idcheck;
            try
            {

                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    db.KCONFIG_AML.ToList().Where(l => l.AML_ID_CVE == Id).ToList().ForEach(m =>
                    {
                        m.AML_NO_MONTO = idcheck;
                        m.AML_FE_MOD = DateTime.Now;
                        m.USR_CL_CVEMOD = User.Identity.GetUserName();
                    });

                    db.SaveChanges();
                    R = new ResultadoModels { NotifyType = Enums.eNotify_Type.success, NotifyMsg = "Se actualizo el status correctamente" };
                    BitacoraModels.guardaBitacora(11, "Se actualizó valor prepago: ", valores, User.Identity.GetUserName());
                }
            }
            catch (Exception ex)
            {
                Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }
            return PartialView("_Resultado", R);
        }

        #endregion

        public ActionResult UpdEstatus(int Id, int valor, int idcheck)
        {
            ResultadoModels R = new ResultadoModels();
            string opcion = string.Empty;
            switch (valor)
            {
                case 1:
                    opcion = "RFC TOYOTA";
                    break;
                case 2:
                    opcion = "UMA";
                    break;
                case 3:
                    opcion = "CORREO";
                    break;
                case 4:
                    opcion = "PREPAGO";
                    break;
                default:
                    opcion = "";
                    break;
            }
            string valores = Id + " " + opcion + "; Activo: " + idcheck;
            try
            {

                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    db.KCONFIG_AML.ToList().Where(l => l.AML_ID_CVE == Id).ToList().ForEach(m =>
                    {
                        m.AML_FG_STATUS = idcheck;
                        m.AML_FE_MOD = DateTime.Now;
                        m.USR_CL_CVEMOD = User.Identity.GetUserName();
                    });

                    db.SaveChanges();
                    R = new ResultadoModels { NotifyType = Enums.eNotify_Type.success, NotifyMsg = "Se actualizo el status correctamente" };
                    BitacoraModels.guardaBitacora(11, "Se actualizó status registro, tab: " + opcion, valores, User.Identity.GetUserName());
                }
            }
            catch (Exception ex)
            {
                Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
                R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
            }
            return PartialView("_Resultado", R);
        }

        public ActionResult ModalConfirmar(int AML_ID_CVE, int Param)
        {
            DialogViewModel confirmar = new DialogViewModel();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(AML_ID_CVE.ToString()))
                {
                    confirmar = db.KCONFIG_AML.Where(m => m.AML_ID_CVE == AML_ID_CVE).Select(m => new DialogViewModel
                    {
                        Id = AML_ID_CVE,
                        IdParametro = Param,
                        Email = m.AML_DS_EMAIL,
                        Valor = (decimal)m.AML_NO_MONTO,
                        Descripcion = m.AML_DS_DES
                    }).Single();

                }
            }

            switch (Param)
            {
                case 1:
                    confirmar.TituloModal = "Editar RFC Toyota";
                    break;
                case 2:
                    confirmar.TituloModal = "Editar Umbral Configuraciones";
                    break;
                case 3:
                    confirmar.TituloModal = "Editar Umbral Alertas";
                    break;
                default:
                    confirmar.TituloModal = "";
                    break;
            }
            confirmar.Controlador = "config";
            confirmar.MetodoAccion = "DeleteConfig";
            return PartialView("_ModalConfirmar", confirmar);
        }

        /// <summary>
        /// Este método permite eliminar lógicamente la configuración en la base de datos.
        /// </summary>
        /// <param name="AML_ID_CVE"></param>
        private void DeleteConfig(int AML_ID_CVE)
        {
            string userName = User.Identity.GetUserName();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                db.KCONFIG_AML.ToList()
                    .Where(j => j.AML_ID_CVE == AML_ID_CVE).ToList()
                    .ForEach(i =>
                    {
                        i.AML_FG_STATUS = 3;
                        i.AML_FE_MOD = DateTime.Now;
                        i.USR_CL_CVEMOD = userName;

                    });

                db.SaveChanges();
            }
        }

        public ActionResult _ModalDialog(int AML_ID_CVE, int PAR_CL_CVE)
        {
            DialogViewModel _dialog = new DialogViewModel();
            _dialog.Id = AML_ID_CVE;
            _dialog.IdParametro = PAR_CL_CVE;
            _dialog.Controlador = "config";
            _dialog.MetodoAccion = "UpdateConfig";
            _dialog.TituloModal = "Editar Registro";
            return PartialView("_ModalDialog", _dialog);
        }
    }
}