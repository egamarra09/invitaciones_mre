using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class MenuUsuariosTipoMetadata
    {

        public int IdMenuTipoUsuario { get; set; }
        [DisplayName("Menú")]
        public int IdMenu { get; set; }
        [DisplayName("Tipo Usuario")]
        public int IdUsuarioTipo { get; set; }
        [DisplayName("Menú")]
        public virtual Menu IdMenuNavigation { get; set; } = null!;
        [DisplayName("Tipo Usuario")]
        public virtual UsuariosTipo IdUsuarioTipoNavigation { get; set; } = null!;
    }
}
