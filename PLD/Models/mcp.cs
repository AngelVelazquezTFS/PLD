using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toyota.Financial.SupportApps.Models
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    [System.Xml.Serialization.XmlRootAttribute("archivo", Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc", IsNullable = false)]
    public partial class archivo_type
    {

        private informe_type[] informeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("informe")]
        public informe_type[] informe
        {
            get
            {
                return this.informeField;
            }
            set
            {
                this.informeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class informe_type
    {

        private string mes_reportadoField;

        private sujeto_obligado_type sujeto_obligadoField;

        private aviso_type[] avisoField;

        /// <remarks/>
        public string mes_reportado
        {
            get
            {
                return this.mes_reportadoField;
            }
            set
            {
                this.mes_reportadoField = value;
            }
        }

        /// <remarks/>
        public sujeto_obligado_type sujeto_obligado
        {
            get
            {
                return this.sujeto_obligadoField;
            }
            set
            {
                this.sujeto_obligadoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("aviso")]
        public aviso_type[] aviso
        {
            get
            {
                return this.avisoField;
            }
            set
            {
                this.avisoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class sujeto_obligado_type
    {

        private string clave_entidad_colegiadaField;

        private string clave_sujeto_obligadoField;

        private string clave_actividadField;

        private string exentoField;

        /// <remarks/>
        public string clave_entidad_colegiada
        {
            get
            {
                return this.clave_entidad_colegiadaField;
            }
            set
            {
                this.clave_entidad_colegiadaField = value;
            }
        }

        /// <remarks/>
        public string clave_sujeto_obligado
        {
            get
            {
                return this.clave_sujeto_obligadoField;
            }
            set
            {
                this.clave_sujeto_obligadoField = value;
            }
        }

        /// <remarks/>
        public string clave_actividad
        {
            get
            {
                return this.clave_actividadField;
            }
            set
            {
                this.clave_actividadField = value;
            }
        }

        /// <remarks/>
        public string exento
        {
            get
            {
                return this.exentoField;
            }
            set
            {
                this.exentoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class datos_liquidacion_type
    {

        private string fecha_disposicionField;

        private string instrumento_monetarioField;

        private string monedaField;

        private string monto_operacionField;

        /// <remarks/>
        public string fecha_disposicion
        {
            get
            {
                return this.fecha_disposicionField;
            }
            set
            {
                this.fecha_disposicionField = value;
            }
        }

        /// <remarks/>
        public string instrumento_monetario
        {
            get
            {
                return this.instrumento_monetarioField;
            }
            set
            {
                this.instrumento_monetarioField = value;
            }
        }

        /// <remarks/>
        public string moneda
        {
            get
            {
                return this.monedaField;
            }
            set
            {
                this.monedaField = value;
            }
        }

        /// <remarks/>
        public string monto_operacion
        {
            get
            {
                return this.monto_operacionField;
            }
            set
            {
                this.monto_operacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class fideicomiso_garante_type
    {

        private string denominacion_razonField;

        private string rfcField;

        private string identificador_fideicomisoField;

        /// <remarks/>
        public string denominacion_razon
        {
            get
            {
                return this.denominacion_razonField;
            }
            set
            {
                this.denominacion_razonField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string identificador_fideicomiso
        {
            get
            {
                return this.identificador_fideicomisoField;
            }
            set
            {
                this.identificador_fideicomisoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_moral_garante_type
    {

        private string denominacion_razonField;

        private string fecha_constitucionField;

        private string rfcField;

        /// <remarks/>
        public string denominacion_razon
        {
            get
            {
                return this.denominacion_razonField;
            }
            set
            {
                this.denominacion_razonField = value;
            }
        }

        /// <remarks/>
        public string fecha_constitucion
        {
            get
            {
                return this.fecha_constitucionField;
            }
            set
            {
                this.fecha_constitucionField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_fisica_garante_type
    {

        private string nombreField;

        private string apellido_paternoField;

        private string apellido_maternoField;

        private string fecha_nacimientoField;

        private string rfcField;

        private string curpField;

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        public string apellido_paterno
        {
            get
            {
                return this.apellido_paternoField;
            }
            set
            {
                this.apellido_paternoField = value;
            }
        }

        /// <remarks/>
        public string apellido_materno
        {
            get
            {
                return this.apellido_maternoField;
            }
            set
            {
                this.apellido_maternoField = value;
            }
        }

        /// <remarks/>
        public string fecha_nacimiento
        {
            get
            {
                return this.fecha_nacimientoField;
            }
            set
            {
                this.fecha_nacimientoField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string curp
        {
            get
            {
                return this.curpField;
            }
            set
            {
                this.curpField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class tipo_garante_type
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fideicomiso", typeof(fideicomiso_garante_type))]
        [System.Xml.Serialization.XmlElementAttribute("persona_fisica", typeof(persona_fisica_garante_type))]
        [System.Xml.Serialization.XmlElementAttribute("persona_moral", typeof(persona_moral_garante_type))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class datos_otro_type
    {

        private string descripcion_garantiaField;

        /// <remarks/>
        public string descripcion_garantia
        {
            get
            {
                return this.descripcion_garantiaField;
            }
            set
            {
                this.descripcion_garantiaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class datos_inmueble_type
    {

        private string tipo_inmuebleField;

        private string valor_referenciaField;

        private string codigo_postalField;

        private string folio_realField;

        /// <remarks/>
        public string tipo_inmueble
        {
            get
            {
                return this.tipo_inmuebleField;
            }
            set
            {
                this.tipo_inmuebleField = value;
            }
        }

        /// <remarks/>
        public string valor_referencia
        {
            get
            {
                return this.valor_referenciaField;
            }
            set
            {
                this.valor_referenciaField = value;
            }
        }

        /// <remarks/>
        public string codigo_postal
        {
            get
            {
                return this.codigo_postalField;
            }
            set
            {
                this.codigo_postalField = value;
            }
        }

        /// <remarks/>
        public string folio_real
        {
            get
            {
                return this.folio_realField;
            }
            set
            {
                this.folio_realField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class datos_bien_mutuo_type
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("datos_inmueble", typeof(datos_inmueble_type))]
        [System.Xml.Serialization.XmlElementAttribute("datos_otro", typeof(datos_otro_type))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class datos_garantia_type
    {

        private string tipo_garantiaField;

        private datos_bien_mutuo_type datos_bien_mutuoField;

        private tipo_garante_type tipo_personaField;

        /// <remarks/>
        public string tipo_garantia
        {
            get
            {
                return this.tipo_garantiaField;
            }
            set
            {
                this.tipo_garantiaField = value;
            }
        }

        /// <remarks/>
        public datos_bien_mutuo_type datos_bien_mutuo
        {
            get
            {
                return this.datos_bien_mutuoField;
            }
            set
            {
                this.datos_bien_mutuoField = value;
            }
        }

        /// <remarks/>
        public tipo_garante_type tipo_persona
        {
            get
            {
                return this.tipo_personaField;
            }
            set
            {
                this.tipo_personaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class datos_operacion_type
    {

        private string fecha_operacionField;

        private string codigo_postalField;

        private string tipo_operacionField;

        private datos_garantia_type[] datos_garantiaField;

        private datos_liquidacion_type[] datos_liquidacionField;

        /// <remarks/>
        public string fecha_operacion
        {
            get
            {
                return this.fecha_operacionField;
            }
            set
            {
                this.fecha_operacionField = value;
            }
        }

        /// <remarks/>
        public string codigo_postal
        {
            get
            {
                return this.codigo_postalField;
            }
            set
            {
                this.codigo_postalField = value;
            }
        }

        /// <remarks/>
        public string tipo_operacion
        {
            get
            {
                return this.tipo_operacionField;
            }
            set
            {
                this.tipo_operacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("datos_garantia")]
        public datos_garantia_type[] datos_garantia
        {
            get
            {
                return this.datos_garantiaField;
            }
            set
            {
                this.datos_garantiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("datos_liquidacion")]
        public datos_liquidacion_type[] datos_liquidacion
        {
            get
            {
                return this.datos_liquidacionField;
            }
            set
            {
                this.datos_liquidacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class fideicomiso_simple_type
    {

        private string denominacion_razonField;

        private string rfcField;

        private string identificador_fideicomisoField;

        /// <remarks/>
        public string denominacion_razon
        {
            get
            {
                return this.denominacion_razonField;
            }
            set
            {
                this.denominacion_razonField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string identificador_fideicomiso
        {
            get
            {
                return this.identificador_fideicomisoField;
            }
            set
            {
                this.identificador_fideicomisoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_moral_simple_type
    {

        private string denominacion_razonField;

        private string fecha_constitucionField;

        private string rfcField;

        private string pais_nacionalidadField;

        /// <remarks/>
        public string denominacion_razon
        {
            get
            {
                return this.denominacion_razonField;
            }
            set
            {
                this.denominacion_razonField = value;
            }
        }

        /// <remarks/>
        public string fecha_constitucion
        {
            get
            {
                return this.fecha_constitucionField;
            }
            set
            {
                this.fecha_constitucionField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string pais_nacionalidad
        {
            get
            {
                return this.pais_nacionalidadField;
            }
            set
            {
                this.pais_nacionalidadField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_fisica_simple_type
    {

        private string nombreField;

        private string apellido_paternoField;

        private string apellido_maternoField;

        private string fecha_nacimientoField;

        private string rfcField;

        private string curpField;

        private string pais_nacionalidadField;

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        public string apellido_paterno
        {
            get
            {
                return this.apellido_paternoField;
            }
            set
            {
                this.apellido_paternoField = value;
            }
        }

        /// <remarks/>
        public string apellido_materno
        {
            get
            {
                return this.apellido_maternoField;
            }
            set
            {
                this.apellido_maternoField = value;
            }
        }

        /// <remarks/>
        public string fecha_nacimiento
        {
            get
            {
                return this.fecha_nacimientoField;
            }
            set
            {
                this.fecha_nacimientoField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string curp
        {
            get
            {
                return this.curpField;
            }
            set
            {
                this.curpField = value;
            }
        }

        /// <remarks/>
        public string pais_nacionalidad
        {
            get
            {
                return this.pais_nacionalidadField;
            }
            set
            {
                this.pais_nacionalidadField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class tipo_persona_simple_type
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fideicomiso", typeof(fideicomiso_simple_type))]
        [System.Xml.Serialization.XmlElementAttribute("persona_fisica", typeof(persona_fisica_simple_type))]
        [System.Xml.Serialization.XmlElementAttribute("persona_moral", typeof(persona_moral_simple_type))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class dueno_beneficiario_type
    {

        private tipo_persona_simple_type tipo_personaField;

        /// <remarks/>
        public tipo_persona_simple_type tipo_persona
        {
            get
            {
                return this.tipo_personaField;
            }
            set
            {
                this.tipo_personaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class telefono_type
    {

        private string clave_paisField;

        private string numero_telefonoField;

        private string correo_electronicoField;

        /// <remarks/>
        public string clave_pais
        {
            get
            {
                return this.clave_paisField;
            }
            set
            {
                this.clave_paisField = value;
            }
        }

        /// <remarks/>
        public string numero_telefono
        {
            get
            {
                return this.numero_telefonoField;
            }
            set
            {
                this.numero_telefonoField = value;
            }
        }

        /// <remarks/>
        public string correo_electronico
        {
            get
            {
                return this.correo_electronicoField;
            }
            set
            {
                this.correo_electronicoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class extranjero_type
    {

        private string paisField;

        private string estado_provinciaField;

        private string ciudad_poblacionField;

        private string coloniaField;

        private string calleField;

        private string numero_exteriorField;

        private string numero_interiorField;

        private string codigo_postalField;

        /// <remarks/>
        public string pais
        {
            get
            {
                return this.paisField;
            }
            set
            {
                this.paisField = value;
            }
        }

        /// <remarks/>
        public string estado_provincia
        {
            get
            {
                return this.estado_provinciaField;
            }
            set
            {
                this.estado_provinciaField = value;
            }
        }

        /// <remarks/>
        public string ciudad_poblacion
        {
            get
            {
                return this.ciudad_poblacionField;
            }
            set
            {
                this.ciudad_poblacionField = value;
            }
        }

        /// <remarks/>
        public string colonia
        {
            get
            {
                return this.coloniaField;
            }
            set
            {
                this.coloniaField = value;
            }
        }

        /// <remarks/>
        public string calle
        {
            get
            {
                return this.calleField;
            }
            set
            {
                this.calleField = value;
            }
        }

        /// <remarks/>
        public string numero_exterior
        {
            get
            {
                return this.numero_exteriorField;
            }
            set
            {
                this.numero_exteriorField = value;
            }
        }

        /// <remarks/>
        public string numero_interior
        {
            get
            {
                return this.numero_interiorField;
            }
            set
            {
                this.numero_interiorField = value;
            }
        }

        /// <remarks/>
        public string codigo_postal
        {
            get
            {
                return this.codigo_postalField;
            }
            set
            {
                this.codigo_postalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class nacional_type
    {

        private string coloniaField;

        private string calleField;

        private string numero_exteriorField;

        private string numero_interiorField;

        private string codigo_postalField;

        /// <remarks/>
        public string colonia
        {
            get
            {
                return this.coloniaField;
            }
            set
            {
                this.coloniaField = value;
            }
        }

        /// <remarks/>
        public string calle
        {
            get
            {
                return this.calleField;
            }
            set
            {
                this.calleField = value;
            }
        }

        /// <remarks/>
        public string numero_exterior
        {
            get
            {
                return this.numero_exteriorField;
            }
            set
            {
                this.numero_exteriorField = value;
            }
        }

        /// <remarks/>
        public string numero_interior
        {
            get
            {
                return this.numero_interiorField;
            }
            set
            {
                this.numero_interiorField = value;
            }
        }

        /// <remarks/>
        public string codigo_postal
        {
            get
            {
                return this.codigo_postalField;
            }
            set
            {
                this.codigo_postalField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class tipo_domicilio_type
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("extranjero", typeof(extranjero_type))]
        [System.Xml.Serialization.XmlElementAttribute("nacional", typeof(nacional_type))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class fideicomiso_type
    {

        private string denominacion_razonField;

        private string rfcField;

        private string identificador_fideicomisoField;

        private representante_apoderado_type apoderado_delegadoField;

        /// <remarks/>
        public string denominacion_razon
        {
            get
            {
                return this.denominacion_razonField;
            }
            set
            {
                this.denominacion_razonField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string identificador_fideicomiso
        {
            get
            {
                return this.identificador_fideicomisoField;
            }
            set
            {
                this.identificador_fideicomisoField = value;
            }
        }

        /// <remarks/>
        public representante_apoderado_type apoderado_delegado
        {
            get
            {
                return this.apoderado_delegadoField;
            }
            set
            {
                this.apoderado_delegadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class representante_apoderado_type
    {

        private string nombreField;

        private string apellido_paternoField;

        private string apellido_maternoField;

        private string fecha_nacimientoField;

        private string rfcField;

        private string curpField;

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        public string apellido_paterno
        {
            get
            {
                return this.apellido_paternoField;
            }
            set
            {
                this.apellido_paternoField = value;
            }
        }

        /// <remarks/>
        public string apellido_materno
        {
            get
            {
                return this.apellido_maternoField;
            }
            set
            {
                this.apellido_maternoField = value;
            }
        }

        /// <remarks/>
        public string fecha_nacimiento
        {
            get
            {
                return this.fecha_nacimientoField;
            }
            set
            {
                this.fecha_nacimientoField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string curp
        {
            get
            {
                return this.curpField;
            }
            set
            {
                this.curpField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_moral_type
    {

        private string denominacion_razonField;

        private string fecha_constitucionField;

        private string rfcField;

        private string pais_nacionalidadField;

        private string giro_mercantilField;

        private representante_apoderado_type representante_apoderadoField;

        /// <remarks/>
        public string denominacion_razon
        {
            get
            {
                return this.denominacion_razonField;
            }
            set
            {
                this.denominacion_razonField = value;
            }
        }

        /// <remarks/>
        public string fecha_constitucion
        {
            get
            {
                return this.fecha_constitucionField;
            }
            set
            {
                this.fecha_constitucionField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string pais_nacionalidad
        {
            get
            {
                return this.pais_nacionalidadField;
            }
            set
            {
                this.pais_nacionalidadField = value;
            }
        }

        /// <remarks/>
        public string giro_mercantil
        {
            get
            {
                return this.giro_mercantilField;
            }
            set
            {
                this.giro_mercantilField = value;
            }
        }

        /// <remarks/>
        public representante_apoderado_type representante_apoderado
        {
            get
            {
                return this.representante_apoderadoField;
            }
            set
            {
                this.representante_apoderadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_fisica_type
    {

        private string nombreField;

        private string apellido_paternoField;

        private string apellido_maternoField;

        private string fecha_nacimientoField;

        private string rfcField;

        private string curpField;

        private string pais_nacionalidadField;

        private string actividad_economicaField;

        /// <remarks/>
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <remarks/>
        public string apellido_paterno
        {
            get
            {
                return this.apellido_paternoField;
            }
            set
            {
                this.apellido_paternoField = value;
            }
        }

        /// <remarks/>
        public string apellido_materno
        {
            get
            {
                return this.apellido_maternoField;
            }
            set
            {
                this.apellido_maternoField = value;
            }
        }

        /// <remarks/>
        public string fecha_nacimiento
        {
            get
            {
                return this.fecha_nacimientoField;
            }
            set
            {
                this.fecha_nacimientoField = value;
            }
        }

        /// <remarks/>
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }

        /// <remarks/>
        public string curp
        {
            get
            {
                return this.curpField;
            }
            set
            {
                this.curpField = value;
            }
        }

        /// <remarks/>
        public string pais_nacionalidad
        {
            get
            {
                return this.pais_nacionalidadField;
            }
            set
            {
                this.pais_nacionalidadField = value;
            }
        }

        /// <remarks/>
        public string actividad_economica
        {
            get
            {
                return this.actividad_economicaField;
            }
            set
            {
                this.actividad_economicaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class tipo_persona_type
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fideicomiso", typeof(fideicomiso_type))]
        [System.Xml.Serialization.XmlElementAttribute("persona_fisica", typeof(persona_fisica_type))]
        [System.Xml.Serialization.XmlElementAttribute("persona_moral", typeof(persona_moral_type))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class persona_aviso_type
    {

        private tipo_persona_type tipo_personaField;

        private tipo_domicilio_type tipo_domicilioField;

        private telefono_type telefonoField;

        /// <remarks/>
        public tipo_persona_type tipo_persona
        {
            get
            {
                return this.tipo_personaField;
            }
            set
            {
                this.tipo_personaField = value;
            }
        }

        /// <remarks/>
        public tipo_domicilio_type tipo_domicilio
        {
            get
            {
                return this.tipo_domicilioField;
            }
            set
            {
                this.tipo_domicilioField = value;
            }
        }

        /// <remarks/>
        public telefono_type telefono
        {
            get
            {
                return this.telefonoField;
            }
            set
            {
                this.telefonoField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class alerta_type
    {

        private string tipo_alertaField;

        private string descripcion_alertaField;

        /// <remarks/>
        public string tipo_alerta
        {
            get
            {
                return this.tipo_alertaField;
            }
            set
            {
                this.tipo_alertaField = value;
            }
        }

        /// <remarks/>
        public string descripcion_alerta
        {
            get
            {
                return this.descripcion_alertaField;
            }
            set
            {
                this.descripcion_alertaField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class modificatorio_type
    {

        private string folio_modificacionField;

        private string descripcion_modificacionField;

        /// <remarks/>
        public string folio_modificacion
        {
            get
            {
                return this.folio_modificacionField;
            }
            set
            {
                this.folio_modificacionField = value;
            }
        }

        /// <remarks/>
        public string descripcion_modificacion
        {
            get
            {
                return this.descripcion_modificacionField;
            }
            set
            {
                this.descripcion_modificacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uif.shcp.gob.mx/recepcion/mpc")]
    public partial class aviso_type
    {

        private string referencia_avisoField;

        private modificatorio_type modificatorioField;

        private string prioridadField;

        private alerta_type alertaField;

        private persona_aviso_type[] persona_avisoField;

        private dueno_beneficiario_type[] dueno_beneficiarioField;

        private datos_operacion_type[] detalle_operacionesField;

        /// <remarks/>
        public string referencia_aviso
        {
            get
            {
                return this.referencia_avisoField;
            }
            set
            {
                this.referencia_avisoField = value;
            }
        }

        /// <remarks/>
        public modificatorio_type modificatorio
        {
            get
            {
                return this.modificatorioField;
            }
            set
            {
                this.modificatorioField = value;
            }
        }

        /// <remarks/>
        public string prioridad
        {
            get
            {
                return this.prioridadField;
            }
            set
            {
                this.prioridadField = value;
            }
        }

        /// <remarks/>
        public alerta_type alerta
        {
            get
            {
                return this.alertaField;
            }
            set
            {
                this.alertaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("persona_aviso")]
        public persona_aviso_type[] persona_aviso
        {
            get
            {
                return this.persona_avisoField;
            }
            set
            {
                this.persona_avisoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dueno_beneficiario")]
        public dueno_beneficiario_type[] dueno_beneficiario
        {
            get
            {
                return this.dueno_beneficiarioField;
            }
            set
            {
                this.dueno_beneficiarioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("datos_operacion", IsNullable = false)]
        public datos_operacion_type[] detalle_operaciones
        {
            get
            {
                return this.detalle_operacionesField;
            }
            set
            {
                this.detalle_operacionesField = value;
            }
        }
    }

}