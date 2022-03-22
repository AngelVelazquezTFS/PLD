using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLD.Models
{
    public class ConfigViewModels
    {
        [Display(Name = "Id Configuracion")]
        public int AML_ID_CVE { get; set; }

        [Display(Name = "Id Parametro")]
        public int PAR_CL_VALOR { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string AML_DS_DES { get; set; }

        [Required]
        [DisplayName("Valor")]
        public decimal? AML_NO_MONTO { get; set; }

        [DisplayName("Email")]
        public string AML_DS_EMAIL { get; set; }

        [DisplayName("Estatus")]
        public int AML_FG_STATUS { get; set; }

        public String USR_CL_CVEALTA { get; set; }

        [DataType(DataType.Date)]
        public DateTime AML_FE_ALTA { get; set; }

        public String USR_CL_CVEMOD { get; set; }

        [DataType(DataType.Date)]
        public DateTime AML_FE_MOD { get; set; }

        //public String FechaAlta { get; set; }
        //public String FechaModificacion { get; set; }

        public List<ConfigViewModels> ListaConfigRFC { get; set; }
        public List<ConfigViewModels> ListaConfigUMA { get; set; }
        public List<ConfigViewModels> ListaConfigCorreo { get; set; }
        public List<ConfigViewModels> ListaConfigPrepago { get; set; }

        public static List<ConfigViewModels> getConfigs(int par)
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                return db.KCONFIG_AML.Where(m => m.AML_FG_STATUS != 3 && m.PAR_CL_VALOR == par).Select(m => new ConfigViewModels
                {
                    AML_ID_CVE = m.AML_ID_CVE,
                    PAR_CL_VALOR = (int)m.PAR_CL_VALOR,
                    AML_DS_DES = m.AML_DS_DES,
                    AML_NO_MONTO = (decimal)m.AML_NO_MONTO,
                    AML_DS_EMAIL = m.AML_DS_EMAIL,
                    AML_FG_STATUS = (int)m.AML_FG_STATUS,
                    USR_CL_CVEALTA = m.USR_CL_CVEALTA,
                    AML_FE_ALTA = (DateTime)m.AML_FE_ALTA,
                    USR_CL_CVEMOD = m.USR_CL_CVEMOD,
                    AML_FE_MOD = (DateTime)m.AML_FE_MOD,
                    //FechaAlta = ((DateTime)m.AML_FE_ALTA).ToShortDateString(),
                    //FechaModificacion = ((DateTime)m.AML_FE_MOD).ToShortDateString()
                }).ToList();
            }
        }


    }

    public class UMAViewModel
    {
        [Display(Name = "Id Configuracion")]
        public int AML_ID_CVE { get; set; }

        [Display(Name = "Id Parametro")]
        public int PAR_CL_VALOR { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string AML_DS_DES { get; set; }

        [Required]
        [DisplayName("Valor")]
        public decimal? AML_NO_MONTO { get; set; }

        [DisplayName("Estatus")]
        public int AML_FG_STATUS { get; set; }

        public String USR_CL_CVEMOD { get; set; }

        [DataType(DataType.Date)]
        public DateTime AML_FE_MOD { get; set; }

        public List<UMAViewModel> ListUMA { get; set; }

        public static List<UMAViewModel> getListUMA(int par)
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                return db.KCONFIG_AML.Where(m => m.AML_FG_STATUS != 3 && m.PAR_CL_VALOR == par).Select(m => new UMAViewModel
                {
                    AML_ID_CVE = m.AML_ID_CVE,
                    PAR_CL_VALOR = (int)m.PAR_CL_VALOR,
                    AML_DS_DES = m.AML_DS_DES,
                    AML_NO_MONTO = (decimal)m.AML_NO_MONTO,
                    AML_FG_STATUS = (int)m.AML_FG_STATUS,
                    USR_CL_CVEMOD = m.USR_CL_CVEMOD,
                    AML_FE_MOD = (DateTime)m.AML_FE_MOD,
                }).ToList();
            }
        }
    }

}