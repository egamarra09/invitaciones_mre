using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class Institucione
    {
        public Institucione()
        {
            EventoInvitados = new HashSet<EventoInvitado>();
            Grupos = new HashSet<Grupo>();
            Personas = new HashSet<Persona>();
        }

        public int IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<EventoInvitado> EventoInvitados { get; set; }
        public virtual ICollection<Grupo> Grupos { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
