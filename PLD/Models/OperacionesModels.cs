using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SpreadsheetLight;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace PLD.Models
{
    #region OperacionesCargaExcelRemarketing_PlanPiso
    public class OperacionesModels
    {
        public List<ListaParaMetrosExcel> listaDatos = new List<ListaParaMetrosExcel>();
        public IList<BuscarCamposModels> Campos { get; set; }
        public int TipoCarga { get; set; }
        public OperacionesModels()
        {
            Campos = new List<BuscarCamposModels>();
        }
        public List<ListaParaMetrosExcel> leerArchivo(SLDocument archivo)
        {
            try
            {
                SLWorksheetStatistics stats = archivo.GetWorksheetStatistics();
                List<ListaParaMetrosExcel> lista = new List<ListaParaMetrosExcel>();
                for (int row = 2; row <= stats.EndRowIndex; row++)
                {
                    ListaParaMetrosExcel NewAddDomi = new ListaParaMetrosExcel()
                    {
                        Dealer = Convert.ToInt32(archivo.GetCellValueAsString(row, 1).Trim()),
                        RazonSocial = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 2)) ? " " : archivo.GetCellValueAsString(row, 2)),
                        FechaContitucion = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 3)) ? " " : archivo.GetCellValueAsString(row, 3)),
                        RFC = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 4)) ? " " : archivo.GetCellValueAsString(row, 4)),
                        Nacionalidad = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 5)) ? " " : archivo.GetCellValueAsString(row, 5)),
                        GiroMercantil = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 6)) ? " " : archivo.GetCellValueAsString(row, 6)),
                        Nombre = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 7)) ? " " : archivo.GetCellValueAsString(row, 7)),
                        Ap_Paterno = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 8)) ? " " : archivo.GetCellValueAsString(row, 8)),
                        Ap_Materno = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 9)) ? " " : archivo.GetCellValueAsString(row, 9)),
                        FechaNacimiento = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 10)) ? " " : archivo.GetCellValueAsString(row, 10)),
                        RFC1 = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 11)) ? " " : archivo.GetCellValueAsString(row, 11)),
                        Curp = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 12)) ? " " : archivo.GetCellValueAsString(row, 12)),
                        CP = Convert.ToInt32(archivo.GetCellValueAsString(row, 13).Trim()),
                        Estado = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 14)) ? " " : archivo.GetCellValueAsString(row, 14)),
                        Municipio = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 15)) ? " " : archivo.GetCellValueAsString(row, 15)),
                        Colonia = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 16)) ? " " : archivo.GetCellValueAsString(row, 16)),
                        Calle = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 17)) ? " " : archivo.GetCellValueAsString(row, 17)),
                        N_Exterior = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 18)) ? " " : archivo.GetCellValueAsString(row, 18)),
                        N_Interior = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 19)) ? " " : archivo.GetCellValueAsString(row, 19)),
                        N_Telefono = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 20)) ? " " : archivo.GetCellValueAsString(row, 20)),
                        Correo = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 21)) ? " " : archivo.GetCellValueAsString(row, 21)),
                        Vin = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 22)) ? " " : archivo.GetCellValueAsString(row, 22)),
                        FechaDisposicion = (string.IsNullOrEmpty(archivo.GetCellValueAsString(row, 23)) ? " " : archivo.GetCellValueAsString(row, 23)),
                        MontoOperacion = Convert.ToDecimal(archivo.GetCellValueAsString(row, 24).Trim()),
                        Fecha_creacion = DateTime.Now.ToString(),
                    };
                    lista.Add(NewAddDomi);

                }


                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public class ListaParaMetrosExcel
        {
            public int Dealer { get; set; }
            public string RazonSocial { get; set; }
            public string FechaContitucion { get; set; }
            public string RFC { get; set; }
            public string Nacionalidad { get; set; }
            public string GiroMercantil { get; set; }
            public string Nombre { get; set; }
            public string Ap_Paterno { get; set; }
            public string Ap_Materno { get; set; }
            public string FechaNacimiento { get; set; }
            public string RFC1 { get; set; }
            public string Curp { get; set; }
            public int CP { get; set; }
            public string Estado { get; set; }
            public string Municipio { get; set; }
            public string Colonia { get; set; }
            public string Calle { get; set; }
            public string N_Exterior { get; set; }
            public string N_Interior { get; set; }
            public string N_Telefono { get; set; }
            public string Correo { get; set; }
            public string Vin { get; set; }
            public string FechaDisposicion { get; set; }
            public decimal MontoOperacion { get; set; }
            public string Fecha_creacion { get; set; }
            public string Users { get; set; }

        }
        public class BuscarCamposModels
        {
            [Key]
            [Required]
            public string data_name { get; set; }
            public string data_fnJson { get; set; }
            public string Nombre { get; set; }
            public string Tipo { get; set; }
            public bool UsaCatalogo { get; set; }
            public string ActualizaCombo { get; set; }
            public string typeName { get; set; }
            public string methodName { get; set; }
            public bool Todos { get; set; }
            public string TodosMsg { get; set; }
            public bool Required { get; set; }
            public string RequiredMsg { get; set; }
            public string Valor { get; set; }
            public string Formato { get; set; }
            public string StyleClass { get; set; }
            public int Maxlength { get; set; }
            public bool Ninguno { get; set; }
            public IList<HttpPostedFileBase> Archivos { get; set; }
        }
    }
    #endregion

    #region ConsultaMatriz
    public class MatrizRiesgoModels
    {
       
        public string Rfc { get; set; }

        public string IdSolicitud { get; set; }
        public string Solicitud { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public DateTime ValorFechaInicio { get; set; }
        public DateTime ValorFechaFin { get; set; }
        public DateTime ValorMaximo { get; set; }
        public List<ListaParametrosMatriz> ListaMatriz = new List<ListaParametrosMatriz>();

        public List<ListaSolicitudes> Lista_Solicitudes = new List<ListaSolicitudes>();

        public List<ListaParametrosMatriz> ObtieneDatos(string RFC, string IdSolicitud, string FechaInicio, string FechaFin)
        {
            List<ListaParametrosMatriz> lista = new List<ListaParametrosMatriz>();
            List<ListaSolicitudes> listasoli = new List<ListaSolicitudes>();
            EF.DB_Entities db = new EF.DB_Entities();
            var dato  = db.ObtenerMatrizRiesgo(RFC, IdSolicitud,Convert.ToDateTime(FechaInicio), Convert.ToDateTime(FechaFin)).ToList();
            for (int i = 0; i < dato.Count; i++)
            {
                ListaParametrosMatriz NewAddDomi = new ListaParametrosMatriz()
                {
                   Elemento_Riesgo = dato[i].Elemento_Riesgo,
                   Factor_Riesgo = dato[i].Factor_Riesgo,
                   Descripcion = dato[i].Descripcion,
                   Valor =dato[i].Valor,
                   Ponderacion_Indica = Convert.ToDecimal(dato[i].Ponderacion_Indica),
                   Ponderacion_Element = Convert.ToDecimal(dato[i].Ponderacion_Element),
                   Riesgo_Indicador = (string.IsNullOrEmpty(dato[i].Riesgo_Indicador) ? "" : dato[i].Riesgo_Indicador),
                   Valor_Ponderacion_Indica = Convert.ToDecimal(dato[i].Valor_ponderado_Indi),
                   Valor_Ponderacion_Element = Convert.ToDecimal(dato[i].Valor_ponderado_Element),
                   NivelRiesgo = dato[i].NivelRiesgo,
                   ValorRiesgo = dato[i].ValorRiesgo,
                   IdSolicitud = dato[i].IdSolicitud,
                   RFC = dato[i].RFC
                };
                ListaSolicitudes NewAddDomi1 = new ListaSolicitudes()
                {
                    IdSolicitud = dato[i].IdSolicitud,
                    RFC = dato[i].RFC,
                    NivelRiesgo = dato[i].NivelRiesgo,
                    ValorRiesgo = dato[i].ValorRiesgo
                };
                if (lista.Any(a => a.RFC == NewAddDomi1.RFC))
                { 
                }
                else
                {
                    listasoli.Add(NewAddDomi1);
                }
                lista.Add(NewAddDomi);
            }
            Lista_Solicitudes = listasoli;
            return lista;
        }
        public class ListaParametrosMatriz
        {
            public string Elemento_Riesgo { get; set; }
            public string Factor_Riesgo { get; set; }
            public string Descripcion { get; set; }
            public string Valor { get; set; }
            public decimal Ponderacion_Indica { get; set; }
            public decimal Ponderacion_Element { get; set; }
            public string Riesgo_Indicador { get; set; }
            public decimal Valor_Ponderacion_Indica { get; set; }
            public decimal Valor_Ponderacion_Element { get; set; }
            public string NivelRiesgo { get; set; }
            public string ValorRiesgo { get; set; }
            public string IdSolicitud { get; set; }
            public string RFC { get; set; }

        }
        public class ListaSolicitudes        
        {
            public string IdSolicitud { get; set; }
            public string RFC { get; set; }
            public string NivelRiesgo { get; set; }
            public string ValorRiesgo { get; set; }

        }
    }
    #endregion
}