using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Invitaciones.Shared.Data.Metadata
{
	public class EventoMetaData
	{
		[DisplayName("Id Evento")]
		public int IdEvento { get; set; }
		[DisplayName("Tipo de Evento")]
		public int IdTipo { get; set; }
		[DisplayName("Organizador")]
		public int IdOrganizador { get; set; }
		[DisplayName("Categoría")]
		public int IdCategoria { get; set; }
		[DisplayName("Nombre Evento")]
		public string Evento1 { get; set; } = null!;
		[DisplayName("Fecha desde")]
		public DateTime? FechaDesde { get; set; }
		[DisplayName("Fecha hasta")]
		public DateTime? FechaHasta { get; set; }
		[DisplayName("Ubicación latitud")]
		public string? Latitud { get; set; }
		[DisplayName("Ubicación longitud")]
		public string? Longitud { get; set; }
		[DisplayName("Zona horaria")]
		public string? ZonaHoraria { get; set; }
		public string? Formato { get; set; }
		[DisplayName("Cantidad limite")]
		public short? LimitePersonas { get; set; }
		[DisplayName("Hora desde")]
		public string? HoraDesde { get; set; }
		[DisplayName("Hora hasta")]
		public string? HoraHasta { get; set; }
		public string? Descripcion { get; set; }
		public bool Activo { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? UltimaModificacion { get; set; }
	}
}
