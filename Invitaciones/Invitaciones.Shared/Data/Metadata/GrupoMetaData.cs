using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
    public class GrupoMetaData
    {
        public int IdGrupo { get; set; }
        [DisplayName("Institución")]
        public int? IdInstitucion { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public virtual Institucione IdInstitucionNavigation { get; set; }
    }
}
