using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(UsuarioTipoMetaData))]
    public partial class UsuariosTipo
    {
        public UsuariosTipo()
        {
            MenuUsuariosTipos = new HashSet<MenuUsuariosTipo>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdUsuarioTipo { get; set; }
        public string UsuarioTipo { get; set; }

        public virtual ICollection<MenuUsuariosTipo> MenuUsuariosTipos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
