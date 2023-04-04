using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoInvitacionesHistory
    {
        public int IdInvitacion { get; set; }
        public int IdEvento { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
