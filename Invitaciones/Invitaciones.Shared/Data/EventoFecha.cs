using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoFecha
    {
        public int IdEventoFechas { get; set; }
        public int? IdEvento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string HoraDesde { get; set; }
        public string HoraHasta { get; set; }
        public bool? TodoDia { get; set; }
        public bool? Activo { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
    }
}
