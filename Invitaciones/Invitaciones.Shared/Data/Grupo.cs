using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(GrupoMetaData))]
    public partial class Grupo
    {
        public Grupo()
        {
            Personas = new HashSet<Persona>();
        }

        public int IdGrupo { get; set; }
        public int? IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        [JsonIgnore]
        public virtual Institucione IdInstitucionNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
