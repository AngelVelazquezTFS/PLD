using System.Collections.Generic;
using System.Web.Mvc;

namespace PLD.Models
{
    public class ResultadoModels
    {
        public string ToURL { get; set; }
        public Enums.eNotify_Type NotifyType { get; set; }
        public string NotifyMsg { get; set; }
        public string ScriptJS { get; set; }
        public string MSG_SiNo { get; set; }
        public string MSG_SiNoTitulo { get; set; }
        public ICollection<ModelState> ModelState { get; set; }
        public string divLoad { get; set; }
        public bool Dialogo { get; set; }
        public bool DialogoMostrar { get; set; }
        public string DialogoURL { get; set; }

        public ResultadoModels()
        {
            NotifyType = Enums.eNotify_Type.warning;
            NotifyMsg = "Error";
        }
    }
}