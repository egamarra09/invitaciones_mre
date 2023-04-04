using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoInvitadosHistory
    {
        public int IdEventoParticipante { get; set; }
        public int? IdEvento { get; set; }
        public int? IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
