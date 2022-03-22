using PLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PLD.Controllers
{
    public class UMAController : Controller
    {
        // GET: UMA
        public ActionResult ListaUMA()
        {
            ViewBag.Title = "Configuración UMA";
            return PartialView(UMAViewModels.getUMA());
        }

        public ActionResult RegisterUMA(int UMA_ID_CVE)
        {
            UMAViewModels _uma;
            string pass = System.Web.Security.Membership.GeneratePassword(10, 2);
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                if (!string.IsNullOrEmpty(UMA_ID_CVE.ToString()))
                {
                    _uma = db.KUMA.Where(m => m.UMA_FL_CVE == UMA_ID_CVE.ToString()).Select(m => new UMAViewModels
                    {
                        UMA_DS_DES = m.UMA_DS_DES,
                        UMA_NO_MTO = (decimal)m.UMA_NO_MTO,
                        UMA_FE_INICIO = ((DateTime)m.UMA_FE_INICIO).ToString(),
                        UMA_FE_FIN = ((DateTime)m.UMA_FE_FIN).ToString()
                    }).Single();

                    ViewBag.TitleRegEdit = "Editar UMA: ";
                }
                else
                {
                    _uma = new UMAViewModels { UMA_ID_CVE = UMA_ID_CVE };
                    ViewBag.TitleRegEdit = "Registrar Usuario";
                }
            }
            return PartialView("RegisterUMA", UMA_ID_CVE);
        }

        [HttpPost]
        public ActionResult RegisterUMA(UMAViewModels model)
        {
            ViewBag.TitleRegEdit = "Registrar Coniguración";
            string Permisos = string.Empty;

            model.UMA_FE_INICIO = DateTime.Now.ToShortDateString();
            model.UMA_FE_FIN = DateTime.Now.ToShortDateString();
            ResultadoModels R = new ResultadoModels();
            if (ModelState.IsValid)
            {
                try
                {
                    using (EF.DB_Entities db = new EF.DB_Entities())
                    {
                        db.KUMA.Add(new EF.KUMA()
                        {
                            UMA_DS_DES = model.UMA_DS_DES,
                            UMA_NO_MTO = model.UMA_NO_MTO,
                            UMA_FE_INICIO = Convert.ToDateTime(model.UMA_FE_INICIO),
                            UMA_FE_FIN = Convert.ToDateTime(model.UMA_FE_FIN)
                        });
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Logs.Log("--> Register :: --> EXCEPTION: " + ex.ToString(), true);
                    R = new ResultadoModels { NotifyMsg = ex.InnerException == null ? ex.Message : ex.InnerException.Message, NotifyType = Enums.eNotify_Type.danger };
                }
                R = new ResultadoModels { NotifyMsg = "Configuración Registrada con éxito", NotifyType = Enums.eNotify_Type.success };
            }
            else
            {
                R.ModelState = ModelState.Values.Where(e => e.Errors.Count() > 0).ToList();
            }
            return PartialView("_Resultado", R);
        }
    }
}