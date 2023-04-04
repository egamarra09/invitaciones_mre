using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(PersonaMetaData))]
    public partial class Persona
    {
        public Persona()
        {
            EventoInvitados = new HashSet<EventoInvitado>();
        }

        public int IdInvitado { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Titulo { get; set; }
        public string Trato { get; set; }
        public bool? Activo { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
        public virtual Institucione IdInstitucionNavigation { get; set; }
        public virtual ICollection<EventoInvitado> EventoInvitados { get; set; }
    }
}
