using System;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.TicketUnitario.Models
{
	public class TicketAtenderViewModel
	{
        public int Id { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaTermino { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? ClaveEncriptada { get; set; }
    }
}

