using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class EventoInvitacioneMetaData
    {
        public int IdInvitacion { get; set; }
        [DisplayName("Evento")]
        public int IdEvento { get; set; }
    }
}
