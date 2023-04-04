using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(MenuMetaData))]
    public partial class Menu
    {
        public Menu()
        {
            MenuUsuariosTipos = new HashSet<MenuUsuariosTipo>();
        }

        public int IdMenu { get; set; }
        public int IdMenuSuperior { get; set; }
        public string MenuTitulo { get; set; }
        public string Link { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<MenuUsuariosTipo> MenuUsuariosTipos { get; set; }
    }
}
