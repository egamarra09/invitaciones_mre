using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(MenuUsuariosTipoMetaData))]
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdUsuarioTipo { get; set; }
        public bool Activo { get; set; }
        public string Login { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? UltimaModificacion { get; set; }

        public virtual UsuariosTipo IdUsuarioTipoNavigation { get; set; }
    }
}
