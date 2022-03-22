using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLD.Models
{
    public class ModalViewModel
    {
        public int Id { get; set; }

        public string MensajeConfirmacion { get; set; }

        public string Controlador { get; set; }

        public string MetodoAccion { get; set; }

        public string TituloModal { get; set; }

        public string Icono { get; set; }
    }

    public class DialogViewModel
    {
        public int Id { get; set; }

        public int IdParametro { get; set; }

        public string MensajeConfirmacion { get; set; }

        public string Controlador { get; set; }

        public string MetodoAccion { get; set; }

        public string TituloModal { get; set; }

        public string Descripcion { get; set; }

        public decimal Valor { get; set; }

        public string Email { get; set; }
    }
}