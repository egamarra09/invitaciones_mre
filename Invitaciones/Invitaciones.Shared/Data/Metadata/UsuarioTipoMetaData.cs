using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class UsuarioTipoMetaData
    {
        [DisplayName("IdUsuarioTipo")]
        public int IdUsuarioTipo { get; set; }
        [DisplayName("Tipo Usuario")]
        public string? UsuarioTipo { get; set; }
    }
}
