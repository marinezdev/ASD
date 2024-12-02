using System;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Areas.Ticket.Models.Consulta
{
	public class SignatureTicketViewModel
	{
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public string? Firma { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}

