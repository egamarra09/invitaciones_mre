using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class PersonaMetaData
    {
        [DisplayName("IdUsuarioTipo")]
        public int IdInvitado { get; set; }
        [DisplayName("Grupo")]
        public int? IdGrupo { get; set; }
        [DisplayName("Institución")]
        public int? IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Titulo { get; set; }
        public string Trato { get; set; }
        public bool? Activo { get; set; }
    }
}
