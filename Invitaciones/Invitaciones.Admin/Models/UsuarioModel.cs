using System.ComponentModel;

namespace Invitaciones.Admin.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        [DisplayName("Tipo Usuario")]
        public int IdUsuarioTipo { get; set; }
        public bool Activo { get; set; }
        public string? Login { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? UltimaModificacion { get; set; }
    }
}
