using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoParticipante
    {
        public int IdEventoParticipante { get; set; }
        public int? IdEvento { get; set; }
        public int? IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
        public virtual Institucione IdInstitucionNavigation { get; set; }
    }
}
