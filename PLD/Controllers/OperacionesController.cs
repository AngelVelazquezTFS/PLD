using PLD.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace PLD.Controllers
{
    public class OperacionesController : Controller
    {
        // GET: Operaciones
        public ActionResult CargaRemarketing()
        {
            ViewBag.Title = "Remarketing";
            OperacionesModels OP = new OperacionesModels();
            OP.Campos.Add(new OperacionesModels.BuscarCamposModels { data_name = "files", Nombre = "files", Tipo = "file"});
            OP.TipoCarga = 1;
            return PartialView(OP);
        }
        public ActionResult CargaPlanPiso()
        {
            ViewBag.Title = "Plan Piso";
            OperacionesModels OP = new OperacionesModels();
            OP.Campos.Add(new OperacionesModels.BuscarCamposModels { data_name = "files", Nombre = "files", Tipo = "file" });
            OP.TipoCarga = 2;
            return PartialView("CargaRemarketing",OP);
        }
        [HttpPost]
        public ActionResult RemarketingCarga(OperacionesModels modelo)
        {
           
            ResultadoModels R = new ResultadoModels();
            MemoryStream xmlStream = new MemoryStream();
            HttpPostedFileBase file = modelo.Campos[0].Archivos.FirstOrDefault();
            string message = string.Empty;
            try
            {
               
                if (file != null)
                {
                    using (SLDocument Excel = new SLDocument(file.InputStream))
                    {
                        modelo.listaDatos = modelo.leerArchivo(Excel);
                    }
                    EF.DB_Entities db = new EF.DB_Entities();
                    if (modelo.listaDatos.Count >0)
                    {
                        var borra = from x in db.CREMARK_PLANPISO_AML
                                      where x.Remarketing_Plan == (modelo.TipoCarga == 1 ? "Remarketing" : "PlanPiso")
                                    select x;

                        db.CREMARK_PLANPISO_AML.RemoveRange(borra);
                        db.SaveChanges();

                        foreach (var item in modelo.listaDatos)
                        {
                            EF.CREMARK_PLANPISO_AML newCon = new EF.CREMARK_PLANPISO_AML()
                            {
                                Dealer = item.Dealer,
                                RazonSocial = item.RazonSocial,
                                FechaContitucion = item.FechaContitucion,
                                RFC = item.RFC,
                                Nacionalidad = item.Nacionalidad,
                                GiroMercantil = item.GiroMercantil,
                                Nombre = item.Nombre,
                                Ap_Paterno = item.Ap_Materno,
                                Ap_Materno = item.Ap_Materno,
                                FechaNacimiento = item.FechaNacimiento,
                                RFC1 = item.RFC1,
                                Curp = item.Curp,
                                CP = item.CP,
                                Estado = item.Estado,
                                Municipio = item.Municipio,
                                Colonia = item.Colonia,
                                Calle = item.Calle,
                                N_Exterior = item.N_Exterior,
                                N_Interior = item.N_Interior,
                                N_Telefono = item.N_Telefono,
                                Correo = item.Correo,
                                Vin = item.Vin,
                                FechaDisposicion = item.FechaDisposicion,
                                MontoOperacion = item.MontoOperacion,
                                Fecha_creacion = item.Fecha_creacion,
                                Users = "Procesos",
                                Remarketing_Plan = (modelo.TipoCarga==1? "Remarketing" : "PlanPiso")

                            };
                            db.CREMARK_PLANPISO_AML.Add(newCon);
                            db.SaveChanges();

                        }
                    }

                    //BitacoraModels.guardaBitacora(1008, "Se cargo el archivo con exito!! ", modelo.TipoCarga == 1 ? "Remarketing" : "PlanPiso", User.Identity.GetUserName());
                    BitacoraModels.guardaBitacora(1008, "Se cargo el archivo con exito!! " +" - Nombre Archivo: "+ file.FileName.ToString() +" -  Numero de Registros:" + modelo.listaDatos.Count, modelo.TipoCarga == 1 ? "Remarketing" : "PlanPiso", "Procesos");
                    R = new ResultadoModels 
                    { 
                        NotifyMsg = "Se cargo el archivo con exito!!.", 
                        NotifyType = Enums.eNotify_Type.success 
                    };
                }
                else
                {
                    R = new ResultadoModels
                    {
                        NotifyMsg = "Favor de seleccionar un archivo.",
                        NotifyType = Enums.eNotify_Type.warning
                    };
                    message=string.Format("Favor de seleccionar un archivo.");
                    ModelState.AddModelError("", message);
                }
            }
            catch (Exception ex)
            {
                Logs Logs = new Logs();
                R = new ResultadoModels
                {
                    NotifyMsg = "Error al cargar el archivo!!.",
                    NotifyType = Enums.eNotify_Type.warning
                };
                message = string.Format("Error al cargar el archivo!!.");
                ModelState.AddModelError("", message);
                Logs.Log("--> Error al cargar el archivo : --> EXCEPTION: " + ex.Message, true);
            }
            R.divLoad = "mdlAlert";
            return PartialView("_Resultado", R);
       
            
        }
        

        public ActionResult ConsultaMatrizRiesgo()
        {
            ViewBag.Title = "Consulta Matriz Riesgo";
            MatrizRiesgoModels con = new MatrizRiesgoModels();
            con.ValorFechaInicio= DateTime.Now;
            con.ValorFechaFin = DateTime.Now;
            con.ValorMaximo = DateTime.Now.AddDays(30);
            return PartialView(con);
        }
        [HttpPost]
        public ActionResult ConsultaMatriz(MatrizRiesgoModels M)
        {
            ResultadoModels R = new ResultadoModels();

            if (String.IsNullOrEmpty(M.IdSolicitud) && String.IsNullOrEmpty(M.Rfc) && M.FechaInicio.Equals("01-01-1900") && M.FechaFin.Equals("01-01-1900"))
            {
                R = new ResultadoModels
                {
                    NotifyMsg = "Favor de llenar por lo menos un parametro de busqueda",
                    NotifyType = Enums.eNotify_Type.warning
                };
                return PartialView("_Resultado", R);
            }
            else
            {
                M.ListaMatriz = M.ObtieneDatos(M. Rfc,M.IdSolicitud,M.FechaInicio,M.FechaFin);
                if (M.ListaMatriz.Count > 0)
                {
                    
                    return PartialView("ConsultaMatrizRiesgo", M);
                }
                else
                {
                    R = new ResultadoModels
                    {
                        NotifyMsg = "No se encontraron registros con los parametros de busqueda",
                        NotifyType = Enums.eNotify_Type.warning
                    };
                    return PartialView("_Resultado", R);
                }
            }

        }

        [HttpPost]
        public ActionResult DescargaExcel(MatrizRiesgoModels M)
        {
            DataTable dt = new DataTable();
            string titulo = "Matriz de Riesgo "+ DateTime.Now;
            dt.TableName = "datos";
            if (string.IsNullOrEmpty(M.Solicitud))
            {
                M.ListaMatriz = M.ObtieneDatos(
                string.IsNullOrEmpty(M.Rfc) ? "" : M.Rfc,
                string.IsNullOrEmpty(M.IdSolicitud) ? "" : M.IdSolicitud,
                string.IsNullOrEmpty(M.FechaInicio) ? "" : M.FechaInicio,
                string.IsNullOrEmpty(M.FechaFin) ? "" : M.FechaFin
                );
            }
            else
            {
                M.ListaMatriz = M.ObtieneDatos("",M.Solicitud, M.FechaInicio, M.FechaFin);

            }
            
            if (M.ListaMatriz.Count > 0)
            {
                dt.Columns.Add("IdSolicitud ", typeof(string));
                dt.Columns.Add("RFC ", typeof(string));
                dt.Columns.Add("Elemento Riesgo ", typeof(string));
                dt.Columns.Add("Factor Riesgo ", typeof(string));
                dt.Columns.Add("Descripcion ", typeof(string));
                dt.Columns.Add("Valor ", typeof(string));
                dt.Columns.Add("Ponderacion_Indica ", typeof(string));
                dt.Columns.Add("Ponderacion_Element ", typeof(string));
                dt.Columns.Add("Riesgo_Indicador ", typeof(string));
                dt.Columns.Add("Valor_Ponderacion_Indica ", typeof(string));
                dt.Columns.Add("Valor_Ponderacion_Element ", typeof(string));
                dt.Columns.Add("NivelRiesgo ", typeof(string));
                dt.Columns.Add("ValorRiesgo ", typeof(string));
                foreach (var item in M.ListaMatriz)
                {
                    dt.Rows.Add(
                         item.IdSolicitud,
                        item.RFC,
                        item.Elemento_Riesgo,
                        item.Factor_Riesgo,
                        item.Descripcion,
                        item.Valor,
                        item.Ponderacion_Indica,
                        item.Ponderacion_Element,
                        item.Riesgo_Indicador,
                        item.Valor_Ponderacion_Indica,
                        item.Valor_Ponderacion_Element,
                        item.NivelRiesgo,
                        item.ValorRiesgo
                       
                        );
                }
              
            }
            //Document.ImportDataTable(1, 1, dt, true);
            //Document.SaveAs(pathFile);

            XLWorkbook wb = new XLWorkbook();
            MemoryStream stream = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                wb.Worksheets.Add(dt);
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", titulo + ".xlsx");
            }
            else
            {
                dt = new DataTable(titulo);
                wb.Worksheets.Add(dt);
                wb.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", titulo + ".xlsx");
            }

        }

        [HttpPost]
        public ActionResult VerDetalle(MatrizRiesgoModels M)
        {
            ResultadoModels R = new ResultadoModels();

            M.ListaMatriz = M.ObtieneDatos("", M.Solicitud, M.FechaInicio, M.FechaFin);
            if (M.ListaMatriz.Count > 0)
            {

              return PartialView("_VerDetalle", M);
            }
           else
            {
                    R = new ResultadoModels
                    {
                        NotifyMsg = "No se encontraron registros con los parametros de busqueda",
                        NotifyType = Enums.eNotify_Type.warning
                    };
                    return PartialView("_Resultado", R);
            }
            

        }

    }
}