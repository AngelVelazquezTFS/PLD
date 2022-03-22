using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLD.Models
{
    public class Enums
    {
        public enum eNotify_Type : byte
        {
            [Display(Name = "none", ShortName = "none")]
            none = 0,
            [Display(Name = "success", ShortName = "success")]
            success = 1,
            [Display(Name = "warning", ShortName = "warning")]
            warning = 2,
            [Display(Name = "danger", ShortName = "danger")]
            danger = 3
        }

        public enum TipoPersona
        {
            PF = 1,
            PFAE = 3,
            PM = 2
        }

        public enum BasesDatosBC
        {
            TOYOTA_APS = 1,
            BUROCREDITO = 2
        }

        public enum EstatusRegistros
        {
            ACTIVO = 1,
            INACTIVO = 2,
            ELIMINADO = 3
        }

        public enum TipoConfiguracion
        {
            RFC = 1,
            UMAS = 2,
            CORREOS = 3,
            PREPAGOS = 4
        }
    }
}