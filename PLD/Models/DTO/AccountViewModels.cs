using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLD.Models.DTO
{
    public class PermisosModels
    {
        public int id { get; set; }
        public string text { get; set; }
        public string accion { get; set; }
        public string controlador { get; set; }
        public int idMenuPadre { get; set; }
        public byte? nivel { get; set; }
        public string icono { get; set; }
        public bool @checked { get; set; }
        public bool hasChildren { get; set; }
        public virtual List<PermisosModels> children { get; set; }

    }
}