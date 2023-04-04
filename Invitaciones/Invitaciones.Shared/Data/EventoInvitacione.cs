using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data.Metadata;
using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    [ModelMetadataType(typeof(EventoInvitacioneMetaData))]
    public partial class EventoInvitacione
    {
        public EventoInvitacione()
        {
            EventoInvitados = new HashSet<EventoInvitado>();
        }

        public int IdInvitacion { get; set; }
        public int IdEvento { get; set; }
        public string Titulo { get; set; }
        public string Remitente { get; set; }
        public string Saludo { get; set; }
        public string Motivo { get; set; }
        public string Fecha { get; set; }
        public string Lugar { get; set; }
        public string Direccion { get; set; }
        public string Tenida { get; set; }
        public string TipoConf { get; set; }
        public string TextConf { get; set; }
        public string Archivo { get; set; }
        public bool Enviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public bool Activo { get; set; }
        public string Token { get; set; }
       // public DateTime ValidFrom { get; set; }
        //public DateTime ValidTo { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
        public virtual ICollection<EventoInvitado> EventoInvitados { get; set; }
    }
}
