using PLD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLD.Controllers
{
    public class ArchivoMensualController : Controller
    {
        // GET: ArchivoMensual
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneraArchivoMensual()
        {
            ViewBag.Title = "Generación de Archivo Mensual";
            return PartialView("_GeneraArchivo");
        }

        [HttpPost]
        public bool GeneraArchivoXML(string anio, string mes, string tipoArchivo, string UMA)
        {
            ResultadoModels r = new ResultadoModels();
            ArchivoMensualModels m = new ArchivoMensualModels();
            try
            {
                var valUMA = Convert.ToDecimal(UMA) * 1605;
                var MESREPORTADO = anio + mes;
                var model = m.informeXML(MESREPORTADO, valUMA);

                byte[] xmlFactura = null;

                var result = m.CreaXML(model);
                string name = "";

                if (!string.IsNullOrEmpty(result))
                {
                    var dateTime = DateTime.Now.Date;
                    name = string.Format("{0}_{1}_mpc.xml", MESREPORTADO, dateTime.ToShortDateString());
                    m.insertaArchivosMensuales(name, result, "MPC", "FICO");

                    xmlFactura = System.Text.Encoding.UTF8.GetBytes(result);
                    TempData["file"] = xmlFactura;
                    
                    //name = string.Format("{0}_mpc.xml", MESREPORTADO);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
            //return File(xmlFactura, "text/xml", name);
        }

        [HttpGet]
        public virtual ActionResult Download(string fechaReporte)
        {
            var dateTime = DateTime.Now.Date;
            var name = string.Format("{0}_{1}_mpc.xml", fechaReporte, dateTime.ToShortDateString());
            byte[] data = TempData["file"] as byte[];
            return File(data, "text/xml", name);
        }

        [HttpGet]
        public virtual ActionResult DescargaArchivo(string idArchivoM)
        {
            ArchivoMensualModels m = new ArchivoMensualModels();
            var result = m.consultaArchivosPorId(idArchivoM);
            byte[] xmlFactura = System.Text.Encoding.UTF8.GetBytes(result.ARCHIVO);
            TempData["file"] = xmlFactura;

            byte[] data = TempData["file"] as byte[];

            return File(data, "text/xml", result.NOMBRE_ARCHIVO);
        }

        public ActionResult ConsultaDescarga()
        {
            ArchivoMensualModels m = new ArchivoMensualModels();
            m.consultaArchivos();
            ViewBag.Title = "Consulta y Descarga de Archivos";
            return PartialView("_ConsultaDescarga", m);
        }

        [HttpPost]
        public ActionResult BuscarArchivos(string startdate, string enddate, string tipoAviso)
        {
            ArchivoMensualModels m = new ArchivoMensualModels();
            if (string.IsNullOrEmpty(enddate)) enddate = startdate;
            startdate = startdate.Replace("-", "");
            enddate = enddate.Replace("-", "");
            m.Lista_Archivos = m.consultaArchivosPorParametros(startdate, enddate, tipoAviso);
            ViewBag.Title = "Consulta y Descarga de Archivos";
            return PartialView("_ConsultaDescarga", m);
        }
    }
}