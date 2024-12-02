using System;
namespace ASD.Areas.TicketAtencionMovil.Models
{
	public class CierreClienteViewModel
	{
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public int Pregunta1 { get; set; }
        public int Pregunta2 { get; set; }
        public int Pregunta3 { get; set; }
        public string? Nombre { get; set; }
        public string? Firma { get; set; }
        public string? Observaciones { get; set; }
        public string? Clave { get; set; }
    }
}

