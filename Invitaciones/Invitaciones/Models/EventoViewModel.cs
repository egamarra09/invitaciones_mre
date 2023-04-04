using System;
using System.ComponentModel.DataAnnotations;

namespace Invitaciones.Models
{
    public class EventoViewModel
    {
        public Int64? id { get; set; }

        public String? title { get; set; }

        public String? start { get; set; }

        public String? end { get; set; }

        public bool allDay { get; set; }

        public string url { get; set; }
    }
}
