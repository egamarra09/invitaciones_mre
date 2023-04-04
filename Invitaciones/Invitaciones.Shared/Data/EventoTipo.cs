using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoTipo
    {
        public EventoTipo()
        {
            Eventos = new HashSet<Evento>();
        }

        public int IdTipo { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? UltimaModificacion { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
