using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace PLD.Models
{
    public static class Utilities
    {
        public static bool validaToken(string token, string c, string ip, string usr, string o, string f)
        {

            string usuario = Encoding.UTF8.GetString(Convert.FromBase64String(usr));
            string origen = Encoding.UTF8.GetString(Convert.FromBase64String(o));
            string validar, hashString;

            switch (origen)
            {
                case "APS":
                    string cotizacion = Encoding.UTF8.GetString(Convert.FromBase64String(c));
                    string idPersona = Encoding.UTF8.GetString(Convert.FromBase64String(ip));

                    int minutos = int.Parse(ConfigurationManager.AppSettings["TokenVigenciaMinutos"].ToString()); //30;
                    DateTime inicio = DateTime.UtcNow.AddMinutes(minutos * (-1));
                    DateTime fin = DateTime.UtcNow;


                    for (int i = 0; i <= minutos; i++)
                    {
                        validar = string.Join("[]", new string[] { cotizacion, idPersona, usuario, DateTime.UtcNow.AddMinutes(i * (-1)).ToString("ddMMyyyy_HH:mm"), origen });
                        hashString = getHashSha256(validar);

                        if (hashString.Equals(token))
                            return true;
                    }
                    return false;

                case "FICO":
                    string folioTFSM = Encoding.UTF8.GetString(Convert.FromBase64String(f)); //--Sólo para FICO**
                    validar = string.Join("[]", new string[] { usuario, DateTime.UtcNow.ToString("ddMMyyyy_HH:mm:ss"), origen, folioTFSM });
                    hashString = getHashSha256(validar);

                    if (hashString.Equals(token))
                        return true;
                    return false;

                default: return false;
            }
        }

        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            //return Convert.ToBase64String(hash);
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string ObtieneContrasenaWebServiceWCFBC(string wcfUsuario)
        {
            try
            {
                using (EF.DB_Entities db = new EF.DB_Entities())
                {
                    string passCifrado = db.AccesosWS.Where(u => u.usuario == wcfUsuario && u.id_status == 1).SingleOrDefault().password;
                    return ObtenerString(db.sp_PasswordDec(passCifrado));
                }
            }
            catch (Exception ex)
            {
                Logs.Log("Error: ObtieneContrasenaWebServiceWCFBC :: wcfUsuario = " + wcfUsuario + " ------> EXCEPTION = " + ex.ToString(), true);
                throw ex;
            }
        }

        public static void EnviarCorreo(MailMessage msg)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                msg.Priority = MailPriority.High;
                smtpClient.Host = ConfigurationManager.AppSettings["SmtpHost"];
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(ConfigurationManager.AppSettings["mailuser"], ConfigurationManager.AppSettings["mailpwd"]);                
                smtpClient.Send(msg);
            }
            catch (SmtpException ex)
            {
                Logs.Log("Error: EnviarCorreo SmtpException :: ------> EXCEPTION = " + ex.ToString(), true);
            }
            catch (Exception ex)
            {
                Logs.Log("Error: EnviarCorreo :: ------> EXCEPTION = " + ex.ToString(), true);
            }
        }

        #region funciones para la vista _ReportePF y _ReportePMyPFAE


        //public static string ObtenerNombreCompleto(AppBuroCredito.wcfBuroCredito.Nombre1 nombre)
        //{
        //    return System.Text.RegularExpressions.Regex.Replace(nombre.PrimerNombre + " " + nombre.SegundoNombre + " " + nombre.ApellidoPaterno + " " + nombre.ApellidoMaterno + " " + nombre.ApellidoAdicional, @"\s+", " ");
        //}

        //public static string ObtenerNombreCompleto(AppBuroCredito.wcfBuroCredito.DatosGenerales datosGenerales)
        //{
        //    return @System.Text.RegularExpressions.Regex.Replace(datosGenerales.nombre + " " + datosGenerales.segundoNombre + " " + datosGenerales.apellidoPaterno + " " + datosGenerales.apellidoMaterno, @"\s+", " ");
        //}


        /// <summary>
        /// Esta función permite obtener el tipo de moneda a través de su clave  ejemplo: MX --> PESOS
        /// </summary>
        /// <param name="resumenReporte"></param>
        /// <returns></returns>
        public static string ObtenerTipoModenaDescripcion(string tipoMoneda)
        {
            string tipoMonedaDescripcion = tipoMoneda == "MX" ? "PESOS" : tipoMoneda == "UD" ? "UDIS" : tipoMoneda == "US" ? "DOLARES" : "";
            return tipoMonedaDescripcion;
        }

        /// <summary>
        /// Esta función permite restar 2 valores de tipo string y valida que los valores no esten en null y que sean números validos que restar.
        /// </summary>
        /// <param name="numeroStringA"></param>
        /// <param name="numeroStringB"></param>
        /// <returns></returns>
        public static string ObtenerResta2Valores(string numeroStringA, string numeroStringB)
        {
            string resta = " ";

            if (numeroStringA != null && numeroStringB != null)
            {
                int numeroA = 0;
                int numeroB = 0;
                bool sePuedeConvertirA = int.TryParse(numeroStringA, out numeroA);
                bool sePuedeConvertirB = int.TryParse(numeroStringB, out numeroB);

                if (sePuedeConvertirA == true && sePuedeConvertirB == true)
                {
                    resta = (numeroA - numeroB).ToString();
                }
            }

            return resta;

        }
        public static string ConvertirStringAFormatoEntero(string numeroString)
        {
            string valor = " ";

            if (numeroString != null)
            {
                int numero = 0;

                bool sePuedeConvertir = int.TryParse(numeroString, out numero);

                if (sePuedeConvertir == true)
                {
                    valor = numero.ToString();
                }
            }

            return valor;

        }


        /// <summary>
        /// Esta función permite obtener la sumatoria de 2 valores de tipo moneda
        /// </summary>
        /// <param name="reporte"></param>
        /// <returns></returns>
        public static string ObtenerSumatoria(string valorFijo, string valorRevolvente)
        {
            string valorFijoTemp = string.IsNullOrEmpty(valorFijo) || string.IsNullOrWhiteSpace(valorFijo) ? "0" : valorFijo;
            string valorRevolventeTemp = string.IsNullOrEmpty(valorRevolvente) || string.IsNullOrWhiteSpace(valorRevolvente) ? "0" : valorRevolvente;

            string resultadoFinal = "";
            try
            {
                double resultado = Convert.ToDouble(double.Parse(valorFijoTemp, NumberStyles.Currency)) + Convert.ToDouble(double.Parse(valorRevolventeTemp, NumberStyles.Currency));
                resultadoFinal = resultado.ToString("C", CultureInfo.CurrentCulture);
            }
            catch
            {

            }


            return resultadoFinal;
        }



        public static string ObtenerColorSaldoVencidoDetalle(string saldoVencido, string tipoMoneda)
        {
            string color = "";

            if (tipoMoneda == "MX")
            {
                color = @Utilities.ObtenerColorCampoMayor500(saldoVencido);
            }
            else
            {
                color = @Utilities.ObtenerColorCampoMayor0(saldoVencido);
            }
            return color;
        }

        /// <summary>
        /// Esta función permite validar sí el campo debe ir de color normal o de color rojo.
        /// </summary>
        /// <param name="valorActualFormatoMoneda"></param>
        /// <returns></returns>
        public static string ObtenerColorCampoMayor500(string valorActualFormatoMoneda)
        {
            return ObtenerColor(valorActualFormatoMoneda, 500);
        }

        /// <summary>
        /// Esta función permite validar sí el campo debe ir de color normal o de color rojo.
        /// </summary>
        /// <param name="valorActual"></param>
        /// <returns></returns>
        public static string ObtenerColorCampoMayor550(string valorActual)
        {
            return ObtenerColor(valorActual, 550);
        }

        public static string ObtenerColorCampoMayor0(string valorActual)
        {
            return ObtenerColor(valorActual, 0);
        }

        public static string ObtenerColorCampoMayor90(string valorActual)
        {
            string color = "";
            if (string.IsNullOrEmpty(valorActual) == false)
            {
                int numero;
                bool sePuedeConvertir = int.TryParse(valorActual, out numero);

                if (sePuedeConvertir == true)
                {
                    color = int.Parse(valorActual) > 90 ? "red" : "";
                }
            }
            return color;
        }
        public static string ObtenerColorCampoRegistroImpugnado(string valor)
        {
            string color = "";

            if (string.IsNullOrEmpty(valor) == false)
            {
                color = "red";
            }
            return color;
        }

        private static string ObtenerColor(string valorActual, int limite)
        {

            string valor = string.IsNullOrEmpty(valorActual) || string.IsNullOrWhiteSpace(valorActual) ? "0" : valorActual; //Valida sí es nullo 
            double resultado = Convert.ToDouble(double.Parse(valor, NumberStyles.Currency)); //Sí es moneda lo convierte a doble
            string color = resultado > limite ? "red" : "";
            return color;
        }


        public static string ObtenerColorAlertasHawk(string codigoClave)
        {
            string colorHawkCodigoClave = codigoClave == "01" ||
                                          codigoClave == "02" ||
                                          codigoClave == "03" ||
                                          codigoClave == "04" ||
                                          codigoClave == "05" ||
                                          codigoClave == "06" ||
                                          codigoClave == "07" ||
                                          codigoClave == "08" ||
                                          codigoClave == "09" ? "red" : "#504d4d";
            return codigoClave;
        }


        public static string ConvertirFormatoMoneda(string valor)
        {
            string resultadoFinal = valor;
            string valorTemp = string.IsNullOrEmpty(valor) || string.IsNullOrWhiteSpace(valor) ? "0" : valor;
            double numero = 0;

            bool sePuedeConvertir = double.TryParse(valor, out numero);

            if (sePuedeConvertir == true)
            {
                double resultado = Convert.ToDouble(double.Parse(valorTemp, NumberStyles.Currency));
                resultadoFinal = resultado.ToString("C", CultureInfo.CurrentCulture);
            }

            return resultadoFinal;
        }

        public static string ObtenerColorNumSolicitudes(string valorActual, int limite)
        {

            string color = "";
            if (string.IsNullOrEmpty(valorActual) == false)
            {
                int numero;
                bool sePuedeConvertir = int.TryParse(valorActual, out numero);

                if (sePuedeConvertir == true)
                {
                    color = int.Parse(valorActual) > limite ? "red" : "";
                }
            }
            return color;
        }


        public static string ConvertirInt(string valorActual)
        {

            string numeroResultado = "0";
            if (string.IsNullOrEmpty(valorActual) == false)
            {
                int numero;
                bool sePuedeConvertir = int.TryParse(valorActual, out numero);

                if (sePuedeConvertir == true)
                {
                    numeroResultado = numero.ToString();
                }
            }
            return numeroResultado;
        }


        public static string ObtenerColor(string palabra, string palabraClave)
        {
            return palabra.ToUpper().Equals(palabraClave.ToUpper()) ? "red" : "";
        }


        /// <summary>
        /// Función utilizada para convertir la fecha de año mes  a mes año con formato de fecha
        /// Ejemplo:  201909 --> A 2019-09
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string ConvertirFechaAnioMesAMesAnio(string fecha)
        {
            string fechaString = fecha;
            try
            {
                int fechaEntera = Convert.ToInt32(ConvertirInt(fecha + "01"));
                DateTime dt;

                if (DateTime.TryParseExact(fechaEntera.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) == true)
                {
                    string mes = dt.Month.ToString().Length == 1 ? "0" + dt.Month.ToString() : dt.Month.ToString();
                    fechaString = dt.Year.ToString() + "-" + mes;
                }
            }
            catch { }

            return fechaString;
        }
        #endregion

        /// <summary>
        /// Esta función permite convertir un objeto objectResult "string" a string
        /// </summary>
        /// <param name="objetResult"></param>
        /// <returns></returns>
        public static string ObtenerString(System.Data.Entity.Core.Objects.ObjectResult<string> objetResult)
        {
            string resultado = "";

            foreach (var elemento in objetResult.ToList())
            {
                resultado = elemento;
            }
            return resultado;

        }
    }
}