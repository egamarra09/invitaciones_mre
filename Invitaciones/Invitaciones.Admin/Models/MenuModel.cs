using Microsoft.AspNetCore.Mvc.Rendering;
using Invitaciones.Shared.Data;
using System.ComponentModel;

namespace Invitaciones.Admin.Models
{
    public class MenuModel
    {       
        public int IdMenu { get; set; }
        [DisplayName("Menú Superior")]
        public int IdMenuSuperior { get; set; }
        [DisplayName("Título")]
        public string MenuTitulo { get; set; } = null!;
        public string Link { get; set; } = null!;
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public IEnumerable<SelectListItem> List { get; set; } = null!;
    }


}
