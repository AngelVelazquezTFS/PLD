using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLD.Models
{
    public class UMAViewModels
    {
        [Display(Name = "Id UMA")]
        public int UMA_ID_CVE { get; set; }

        //[Required]
        //[DisplayName("UMA")]
        //public string UMA_FL_CVE{ get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string UMA_DS_DES{ get; set; }

        [Required]
        [DisplayName("Valor")]
        public decimal UMA_NO_MTO { get; set; }

        [Required]
        [DisplayName("Fecha Inicio")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public string UMA_FE_INICIO { get; set; }

        [Required]
        [DisplayName("Fecha Fin")]
        public string UMA_FE_FIN { get; set; }


        public static List<UMAViewModels> getUMA()
        {
            using (EF.DB_Entities db = new EF.DB_Entities())
            {
                //List<RegisterViewModel> Lista = new List<RegisterViewModel>();
                return db.KUMA.Select(m => new UMAViewModels
                {
                    UMA_ID_CVE = m.UMA_ID_CVE,
                    UMA_DS_DES = m.UMA_DS_DES,
                    UMA_NO_MTO = (decimal)m.UMA_NO_MTO,
                    UMA_FE_INICIO = ((DateTime)m.UMA_FE_INICIO).ToString(),
                    UMA_FE_FIN = ((DateTime)m.UMA_FE_FIN).ToString()
                }).ToList();
            }
        }

    }
}