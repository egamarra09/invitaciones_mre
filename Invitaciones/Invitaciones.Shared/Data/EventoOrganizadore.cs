using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoOrganizadore
    {
        public EventoOrganizadore()
        {
            Eventos = new HashSet<Evento>();
        }

        public int IdOrganizador { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? UltimaModificacion { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
