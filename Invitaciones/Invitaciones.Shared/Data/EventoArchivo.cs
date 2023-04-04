using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoArchivo
    {
        public int IdEventoArchivo { get; set; }
        public int? IdEvento { get; set; }
        public string Archivo { get; set; }
        public string Tipo { get; set; }
        public string Url { get; set; }
        public bool Activo { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
    }
}
