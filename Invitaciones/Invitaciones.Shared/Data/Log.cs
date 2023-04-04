using System;
using System.Collections.Generic;

namespace Invitaciones.Shared.Data
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Level { get; set; }
        public string Ipaddress { get; set; }
        public string Userid { get; set; }
        public string Callsite { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Exception { get; set; }
        public string Logger { get; set; }
        public string RequestUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string Action { get; set; }
    }
}
