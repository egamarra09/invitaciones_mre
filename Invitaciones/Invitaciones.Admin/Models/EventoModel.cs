using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Invitaciones.Admin.Models
{
    public class EventoModel
    {
        public int IdEvento { get; set; }
        public int IdOrganizador{ get; set; }
        public int IdCategoria { get; set; }
        [DisplayName("Evento")]
        public string Evento { get; set; } = null!;
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Latitud { get; set; } = null!;
        public string Longitud { get; set; } = null!;
        public string ZonaHoraria { get; set; } = null!;
        public string Formato { get; set; } = null!;
        public string LimitePersonas { get; set; } = null!;
        public string HoraDesde { get; set; } = null!;
        public string HoraHasta { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public string Descrcipcion { get; set; } = null!;
        public bool Activo { get; set; }
        public IEnumerable<SelectListItem> List { get; set; } = null!;
    }
}
