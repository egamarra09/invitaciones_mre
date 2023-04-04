using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(MenuUsuariosTipoMetaData))]
    public partial class MenuUsuariosTipo
    {
        public int IdMenuTipoUsuario { get; set; }
        public int IdMenu { get; set; }
        public int IdUsuarioTipo { get; set; }

        public virtual Menu IdMenuNavigation { get; set; }
        public virtual UsuariosTipo IdUsuarioTipoNavigation { get; set; }
    }
}
