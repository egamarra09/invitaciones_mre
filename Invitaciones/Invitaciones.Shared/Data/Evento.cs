using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;
using Invitaciones.Shared.Data;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(EventoMetaData))]
    public partial class Evento
    {
        public Evento()
        {
            EventoArchivos = new HashSet<EventoArchivo>();
            EventoFechas = new HashSet<EventoFecha>();
            EventoInvitaciones = new HashSet<EventoInvitacione>();
        }

        public int IdEvento { get; set; }
        public int IdOrganizador { get; set; }
        public int IdCategoria { get; set; }
        public string Evento1 { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string ZonaHoraria { get; set; }
        public string Formato { get; set; }
        public short? LimitePersonas { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? UltimaModificacion { get; set; }
        public int? IdTipo { get; set; }
        public string Lugar { get; set; }
        public string Direccion { get; set; }
        public string Imagen { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string EnlaceVirtual { get; set; }
        public string Repertir { get; set; }
        public DateTime? FechaConfirmacion { get; set; }

        public virtual EventoCategoria IdCategoriaNavigation { get; set; }
        public virtual EventoOrganizadore IdOrganizadorNavigation { get; set; }
        public virtual EventoTipo IdTipoNavigation { get; set; }
        public virtual ICollection<EventoArchivo> EventoArchivos { get; set; }
        public virtual ICollection<EventoFecha> EventoFechas { get; set; }
        public virtual ICollection<EventoInvitacione> EventoInvitaciones { get; set; }
    }
}
