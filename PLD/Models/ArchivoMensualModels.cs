using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Toyota.Financial.SupportApps.DTO;
using Toyota.Financial.SupportApps.Models;
using ValidaXMLvsXSD;

namespace PLD.Models
{
    public class ArchivoMensualModels
    {
        private string xmlrespuesta;

        //private string pathXML = @"D:\inetpub\wwwroot\LavadoDinero\PLD\PLD\xmlCredito.xml";
        //private string pathXSD = @"D:\inetpub\wwwroot\LavadoDinero\PLD\PLD\mpc.xsd";

        private string pathXML = @"C:\Users\ex_tejal\OneDrive - TMNA\Documentos\Sistemas\PLD\PLD\xmlCredito.xml";
        private string pathXSD = @"C:\Users\ex_tejal\OneDrive - TMNA\Documentos\Sistemas\PLD\PLD\mpc.xsd";


        public class mpcModel
        {
            public string MESREPORTADO { get; set; }
            public string ID_REF_AVISO { get; set; }
            public string PNA_FL_PERSONA { get; set; }
            public string PNA_CL_PJURIDICA { get; set; }
            public string nombrePF { get; set; }
            public string a_patPF { get; set; }
            public string a_matPF { get; set; }
            public string f_nacPF { get; set; }
            public string rfcPF { get; set; }
            public string curpPF { get; set; }
            public string nacionalidad_PF { get; set; }
            public string act_ecoPF { get; set; }
            public string razon_socialPM { get; set; }
            public string fec_constPM { get; set; }
            public string rfcPM { get; set; }
            public string nacionalidad_PM { get; set; }
            public string giro_mercPM { get; set; }
            public string nombre_repPM { get; set; }
            public string a_patrepPM { get; set; }
            public string a_matrepPM { get; set; }
            public string f_nacrepPM { get; set; }
            public string rfc_repPM { get; set; }
            public string curp_repPM { get; set; }
            public string n_colonia { get; set; }
            public string n_calle { get; set; }
            public string n_cp { get; set; }
            public string n_numExt { get; set; }
            public string n_numInt { get; set; }
            public string tel_cvePais { get; set; }
            public string tel_numTel { get; set; }
            public string tel_email { get; set; }
            public string do_fecOperacion { get; set; }
            public string do_cp { get; set; }
            public string do_tipoOperacion { get; set; }
            public string dg_tipoGarantia { get; set; }
            public string dOtro_descGarantia { get; set; }
            public string dl_fecDisposicion { get; set; }
            public string dl_instrumento { get; set; }
            public string dl_moneda { get; set; }
            public string dl_montoOper { get; set; }

        }

        public class ListaArchivos
        {
            public int ID_ARCHIVO { get; set; }
            public string NOMBRE_ARCHIVO { get; set; }
            public string ARCHIVO { get; set; }
            public string TIPO_AVISO { get; set; }
            public string FOLIO { get; set; }
            public string ESTATUS_FOLIO { get; set; }
            public Nullable<System.DateTime> FECHA_CREACION { get; set; }
            public string USR_CL_CVE { get; set; }
        }

        public List<ListaArchivos> Lista_Archivos = new List<ListaArchivos>();
        public string CreaXML(archivo_type archivo_type)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            xmlNameSpace.Add("schemaLocation", "http://www.uif.shcp.gob.mx/recepcion/mpc mpc.xsd");
            xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            xmlrespuesta = string.Empty;

            XmlSerializer oXmlSerializar = new XmlSerializer(typeof(archivo_type));

            using (var sww = new UtilidadXsdToXml(Encoding.UTF8))
            {
                using (XmlWriter writter = XmlWriter.Create(sww, settings))
                {
                    oXmlSerializar.Serialize(writter, archivo_type, xmlNameSpace);
                    xmlrespuesta = sww.ToString();
                    System.IO.File.WriteAllText(pathXML, xmlrespuesta);

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlrespuesta);
                    xmlrespuesta = doc.InnerXml;
                }
            }

            ValidaXMLvsXSDCredito();

            return xmlrespuesta;
        }

        private void ValidaXMLvsXSDCredito()
        {
            ValidaXMLvsXSD.XmlSchemaSetValidador validador = new XmlSchemaSetValidador();
            try
            {
                if (validador.CompruebaXMLvsXSD("http://www.uif.shcp.gob.mx/recepcion/mpc", pathXSD, pathXML))
                {
                    if (validador.TotalErrores() > 0)
                    {
                        MessageBox.Show("TOTAL ERRORES: " + validador.TotalErrores().ToString(), "Alerta");
                        //MessageBox.Show(validador.GetErrores());
                    }
                    if (validador.TotalAdvertencias() > 0)
                    {
                        MessageBox.Show("TOTAL ADVERTENCIAS: " + validador.TotalAdvertencias().ToString(), "Alerta");
                        //MessageBox.Show(validador.GetAdvertencias());
                    }
                    if (validador.TotalErrores() == 0 && validador.TotalAdvertencias() == 0)
                    {
                        MessageBox.Show("Se ha creado el archivo xmlCredito de manera correcta.", "Mensaje");
                    }
                }
                else
                {
                    MessageBox.Show(validador.GetErrorPrincipal());
                }

            }
            catch (Exception ex)
            {
                //RadMessageBox.Show(this, "Error: " + ex.Message + " ", "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Info, MessageBoxDefaultButton.Button1);
            }
        }

        public archivo_type informeXML(string fecha_ejecucion, decimal UMA)
        {
            var s = new List<mpcModel>();
            var aType = new archivo_type();
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                s = db.sp_ObtieneDatos(fecha_ejecucion, UMA).Select(m => new mpcModel
                {
                    MESREPORTADO = m.MESREPORTADO,
                    ID_REF_AVISO = m.ID_REF_AVISO.ToString(),
                    PNA_FL_PERSONA = m.PNA_FL_PERSONA.ToString(),
                    PNA_CL_PJURIDICA = m.PNA_CL_PJURIDICA.ToString(),
                    nombrePF = m.NOMBRE.ToString().ToUpper(),
                    a_patPF = m.A_PATERNO.ToString().ToUpper(),
                    a_matPF = m.A_MATERNO.ToString().ToUpper(),
                    f_nacPF = m.F_NAC.ToString().ToUpper(),
                    rfcPF = m.RFC.ToString().ToUpper(),
                    curpPF = m.CURP.ToString().ToUpper(),
                    nacionalidad_PF = m.PAIS.ToString().ToUpper(),
                    act_ecoPF = m.ACT_ECONOMICA.ToString().ToUpper(),
                    razon_socialPM = m.RAZON_SOCIAL.ToString().ToUpper(),
                    fec_constPM = m.FEC_CONSTITUCION.ToString().ToUpper(),
                    rfcPM = m.PM_RFC.ToString().ToUpper(),
                    nacionalidad_PM = m.PM_PAIS.ToString().ToUpper(),
                    giro_mercPM = m.PM_GIRO.ToString().ToUpper(),
                    nombre_repPM = m.PM_NOMBRE_REP.ToString().ToUpper(),
                    a_patrepPM = m.PM_APAT_REP.ToString().ToUpper(),
                    a_matrepPM = m.PM_AMAT_REP.ToString().ToUpper(),
                    f_nacrepPM = m.PM_FNAC_REP.ToString().ToUpper(),
                    rfc_repPM = m.PM_RFC_REP.ToString().ToUpper(),
                    curp_repPM = m.PM_CURP_REP.ToString().ToUpper(),
                    n_colonia = m.N_COLONIA.ToString().ToUpper(),
                    n_calle = m.N_CALLE.ToString().ToUpper(),
                    n_cp = m.N_CP.ToString().ToUpper(),
                    n_numExt = m.N_NUMEXT.ToString().ToUpper(),
                    n_numInt = m.N_NUMINT.ToString().ToUpper(),
                    tel_cvePais = m.TEL_CVE_PAIS.ToString().ToUpper(),
                    tel_numTel = m.TEL_NUMERO.ToString().ToUpper(),
                    tel_email = m.EMAIL.ToString().ToUpper(),
                    do_fecOperacion = m.FEC_OPERACION.ToString().ToUpper(),
                    do_cp = m.CP_OPERACION.ToString().ToUpper(),
                    do_tipoOperacion = "402",
                    dg_tipoGarantia = "3",
                    dOtro_descGarantia = "AUTO",
                    dl_fecDisposicion = m.FEC_DISPOSICION,
                    dl_instrumento = m.INSTRUMENTO.ToString().ToUpper(),
                    dl_moneda = m.MONEDA,
                    dl_montoOper = m.MONTO_OPERACION,
                }).ToList();
            }

            if(s.Count != 0)
            {
                var Resultado = obtieneDatosAMLEKIP(s);
                aType.informe = new informe_type[] { Resultado };
            }

            return aType;
        }

        public informe_type obtieneDatosAMLEKIP(List<mpcModel> dsDatos)
        {
            var informe = new informe_type();
            var mesR = dsDatos.Select(s => s.MESREPORTADO).FirstOrDefault().ToString();
            List<string> rfcs = new List<string>();
            var rfcpm = dsDatos.Where(s => s.rfcPM != "").Select(s => s.rfcPM.ToString()).Distinct().ToList();
            var rfcpf = dsDatos.Where(s => s.rfcPF != "").Select(s => s.rfcPF.ToString()).Distinct().ToList();
            if (rfcpf.Count != 0)
            {
                foreach (var pf in rfcpf)
                {
                    if (!string.IsNullOrEmpty(pf))
                        rfcs.Add(pf);
                }
            }

            if (rfcpm.Count != 0)
            {
                foreach (var pm in rfcpm)
                {
                    if (!string.IsNullOrEmpty(pm))
                        rfcs.Add(pm);
                }
            }

            var sujetoObligado = new sujeto_obligado_type();
            sujetoObligado.clave_sujeto_obligado = "TFS011012M18";
            sujetoObligado.clave_actividad = "MPC";
            informe.sujeto_obligado = sujetoObligado;
            var conteo = rfcs.Count; 
            informe.aviso = new aviso_type[conteo];
            var i = -1;

            foreach (var R in rfcs)
            {
                List<datos_operacion_type> listaD = new List<datos_operacion_type>();

                var row = dsDatos.Where(d => (d.rfcPM.ToString() == R.ToString()) || (d.rfcPF.ToString() == R.ToString())).FirstOrDefault();

                //var result = validaDatos(row);
                if (i != conteo) i = i + 1;
                var aviso = new aviso_type();
                var alerta = new alerta_type();
                var personaAviso = new persona_aviso_type();
                var tipoPersona = new tipo_persona_type();
                var tipoDomicilio = new tipo_domicilio_type();
                var telefono = new telefono_type();

                string RFC;
                if (!string.IsNullOrEmpty(row.rfcPM.ToString()))
                    RFC = row.rfcPM.ToString();
                else
                    RFC = row.rfcPF.ToString();

                var total = dsDatos.Where(s => s.rfcPM.ToString() == RFC || s.rfcPF.ToString() == RFC).ToList();
                foreach (var rowt in total)
                {
                    var dOperaciones = new datos_operacion_type();
                    var datosGarantia1 = new datos_garantia_type();
                    datosGarantia1.tipo_garantia = "3";
                    var dLiquidacion1 = new datos_liquidacion_type();
                    dLiquidacion1.fecha_disposicion = rowt.dl_fecDisposicion.ToString().ToUpper();
                    dLiquidacion1.instrumento_monetario = rowt.dl_instrumento.ToString();
                    dLiquidacion1.moneda = rowt.dl_moneda.ToString();
                    dLiquidacion1.monto_operacion = rowt.dl_montoOper.ToString();

                    dOperaciones.fecha_operacion = rowt.do_fecOperacion.ToString();
                    dOperaciones.codigo_postal = rowt.do_cp.ToString();
                    dOperaciones.tipo_operacion = "402";
                    dOperaciones.datos_garantia = new datos_garantia_type[] { datosGarantia1 };
                    dOperaciones.datos_liquidacion = new datos_liquidacion_type[] { dLiquidacion1 };
                    listaD.Add(dOperaciones);
                }

                aviso.detalle_operaciones = listaD.ToArray();

                alerta.tipo_alerta = "100";
                aviso.referencia_aviso = row.ID_REF_AVISO.ToString();


                //Se llena el Nodo de Persona Física o Persona Moral que va dentro del Nodo TipoPersona
                if (!string.IsNullOrEmpty(row.nombrePF) && row.nombrePF != "N/A")
                {
                    var personaFisica = new persona_fisica_type();
                    personaFisica.nombre = row.nombrePF;
                    personaFisica.apellido_paterno = row.a_patPF;
                    personaFisica.apellido_materno = row.a_matPF;
                    if (!string.IsNullOrEmpty(row.f_nacPF) && (string.IsNullOrEmpty(row.rfcPF) || string.IsNullOrEmpty(row.curpPF))) personaFisica.fecha_nacimiento = row.f_nacPF;
                    if (!string.IsNullOrEmpty(row.rfcPF)) personaFisica.rfc = row.rfcPF;
                    if (string.IsNullOrEmpty(personaFisica.rfc)) personaFisica.curp = row.curpPF;
                    personaFisica.pais_nacionalidad = row.nacionalidad_PF;
                    personaFisica.actividad_economica = row.act_ecoPF;
                    tipoPersona.Item = personaFisica;
                }
                else if (!string.IsNullOrEmpty(row.razon_socialPM) && row.razon_socialPM != "N/A")
                {
                    var personaMoral = new persona_moral_type();
                    personaMoral.denominacion_razon = row.razon_socialPM;
                    personaMoral.fecha_constitucion = row.fec_constPM;
                    personaMoral.rfc = row.rfcPM;
                    personaMoral.pais_nacionalidad = row.nacionalidad_PM;
                    personaMoral.giro_mercantil = row.giro_mercPM;

                    var rApoderado = new representante_apoderado_type();
                    rApoderado.nombre = row.nombre_repPM;
                    rApoderado.apellido_paterno = row.a_patrepPM;
                    rApoderado.apellido_materno = row.a_matrepPM;
                    rApoderado.fecha_nacimiento = row.f_nacrepPM;
                    if (!string.IsNullOrEmpty(row.rfc_repPM)) rApoderado.rfc = row.rfc_repPM;
                    if (string.IsNullOrEmpty(row.rfc_repPM)) rApoderado.curp = row.curp_repPM;

                    personaMoral.representante_apoderado = rApoderado;
                    tipoPersona.Item = personaMoral;
                }

                //Se llena el nodo de Domicilio Nacional o Domicilio Extranjero que va dentro del Nodo Tipo Domicilio
                if (!string.IsNullOrEmpty(row.n_colonia))
                {
                    var dNacional = new nacional_type();
                    dNacional.colonia = row.n_colonia;
                    dNacional.calle = row.n_calle;
                    dNacional.numero_exterior = row.n_numExt;
                    if (!string.IsNullOrEmpty(row.n_numInt)) dNacional.numero_interior = row.n_numInt;
                    dNacional.codigo_postal = row.n_cp;
                    tipoDomicilio.Item = dNacional;
                }

                //Se llena el nodo Telefono que va dentro del Nodo Persona Aviso
                if (!string.IsNullOrEmpty(row.tel_numTel))
                {
                    telefono.clave_pais = row.tel_cvePais;
                    telefono.numero_telefono = row.tel_numTel;
                    if (!string.IsNullOrEmpty(row.tel_email)) telefono.correo_electronico = row.tel_email;
                    personaAviso.telefono = telefono;
                }

                //Se llena el nodo de Persona Aviso con los nodos de Tipo Persona, Tipo Domicilio y Telefono
                personaAviso.tipo_persona = tipoPersona;
                personaAviso.tipo_domicilio = tipoDomicilio;

                //Se llena el nodo de aviso que va dentro del nodo padre Informe
                aviso.alerta = alerta;
                aviso.prioridad = "1";
                aviso.persona_aviso = new persona_aviso_type[] { personaAviso };

                informe.aviso[i] = aviso;

            }


            return informe;
        }

        public bool insertaArchivosMensuales(string nombreArchivo, string archivo, string tipoAviso, string usuario)
        {
            try
            {
                EF.KARCHIVOS_MENSUALES karchivo = new EF.KARCHIVOS_MENSUALES()
                {
                    NOMBRE_ARCHIVO = nombreArchivo,
                    ARCHIVO = archivo.ToString(),
                    TIPO_AVISO = tipoAviso,
                    FOLIO = "",
                    ESTATUS_FOLIO = "",
                    FECHA_CREACION = DateTime.Now,
                    USR_CL_CVE = usuario
                };
                EF.DB_Entities db = new EF.DB_Entities();

                db.KARCHIVOS_MENSUALES.Add(karchivo);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false; 
            }
            
        }

        public bool consultaArchivos()
        {
            List<ListaArchivos> listaAr = new List<ListaArchivos>();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                listaAr = db.KARCHIVOS_MENSUALES.Select(m => new ListaArchivos
                {
                     ID_ARCHIVO = m.ID_ARCHIVO,
                     NOMBRE_ARCHIVO = m.NOMBRE_ARCHIVO,
                     TIPO_AVISO = m.TIPO_AVISO,
                     FECHA_CREACION = m.FECHA_CREACION,
                     FOLIO = m.FOLIO,
                     ESTATUS_FOLIO = m.ESTATUS_FOLIO,
                     USR_CL_CVE = m.USR_CL_CVE
                }).ToList();

                Lista_Archivos = listaAr;

            }
            return true;
        }

        public List<ListaArchivos> consultaArchivosPorParametros(string startdate, string enddate, string tipoAviso)
        {
            List<ListaArchivos> listaAr = new List<ListaArchivos>();

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                listaAr = db.sp_ObtieneArchivos(startdate, enddate, tipoAviso).Select(m => new ListaArchivos
                {
                    NOMBRE_ARCHIVO = m.NOMBRE_ARCHIVO,
                    TIPO_AVISO = m.TIPO_AVISO,
                    FECHA_CREACION = m.FECHA_CREACION,
                    FOLIO = m.FOLIO,
                    ESTATUS_FOLIO = m.ESTATUS_FOLIO,
                    USR_CL_CVE = m.USR_CL_CVE

                }).ToList();

                Lista_Archivos = listaAr;

            }
            return listaAr;
        }

        public ListaArchivos consultaArchivosPorId(string idArchivoM)
        {
            ListaArchivos listaAr = new ListaArchivos();
            //var id = int(idArchivoM);

            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                listaAr = db.KARCHIVOS_MENSUALES.Where(m => m.ID_ARCHIVO.ToString() == idArchivoM).Select(m => new ListaArchivos
                {
                    NOMBRE_ARCHIVO = m.NOMBRE_ARCHIVO,
                    ARCHIVO = m.ARCHIVO,
                    TIPO_AVISO = m.TIPO_AVISO,
                    FECHA_CREACION = m.FECHA_CREACION,
                    FOLIO = m.FOLIO,
                    ESTATUS_FOLIO = m.ESTATUS_FOLIO,
                    USR_CL_CVE = m.USR_CL_CVE

                }).FirstOrDefault();
            }
            return listaAr;
        }
    }
}