using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class EventoInvitado
    {
        public int IdEventoParticipante { get; set; }
        public int? IdInstitucion { get; set; }
        public int? IdPersona { get; set; }
        public int? IdEventoInvitacion { get; set; }
        public bool? Confirmado { get; set; }
        public DateTime? FechaConfirmacion { get; set; }
        public string TextoAdicional { get; set; }
        public string Token { get; set; }
        //public DateTime ValidFrom { get; set; }
        //public DateTime ValidTo { get; set; }

        public virtual EventoInvitacione IdEventoInvitacionNavigation { get; set; }
        public virtual Institucione IdInstitucionNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
