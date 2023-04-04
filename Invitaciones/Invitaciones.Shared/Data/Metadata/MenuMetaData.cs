using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class MenuMetaData
    {        
        public int IdMenu { get; set; }
        [DisplayName("Menú Superior")]
        public int IdMenuSuperior { get; set; }
        [DisplayName("Título")]
        public string MenuTitulo { get; set; } = null!;
        public string Link { get; set; } = null!;
        public int Orden { get; set; }
        public bool Activo { get; set; }
    }
}
