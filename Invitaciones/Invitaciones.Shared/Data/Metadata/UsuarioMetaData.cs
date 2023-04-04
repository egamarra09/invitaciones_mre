using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class MenuUsuariosTipoMetaData
    {
        [DisplayName("ID Usuario")]
        public int IdUsuario { get; set; }
        [DisplayName("Tipo Usuario")]
        public int IdUsuarioTipo { get; set; }
        public bool Activo { get; set; }
        public string? Login { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        [DisplayName("Fecha Creación")]
        public DateTime? FechaCreacion { get; set; }
        [DisplayName("Ultima Modificación")]
        public DateTime? UltimaModificacion { get; set; }
        [DisplayName("Tipo Usuario")]
        public virtual UsuariosTipo IdUsuarioTipoNavigation { get; set; } = null!;
    }
}
