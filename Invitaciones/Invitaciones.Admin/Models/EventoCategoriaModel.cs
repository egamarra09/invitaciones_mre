using System.ComponentModel;

namespace Invitaciones.Admin.Models
{
    public class EventoCategoriaModel
    {
        public int IdCategoria { get; set; }
        [DisplayName("Nombre Categoría")]
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
