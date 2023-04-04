using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoCategoria
    {
        public EventoCategoria()
        {
            Eventos = new HashSet<Evento>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? UltimaModificacion { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
