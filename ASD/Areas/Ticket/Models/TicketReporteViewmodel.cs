using System;
namespace ASD.Areas.Ticket.Models
{
	public class TicketReporteViewmodel
	{
        public int? Id { get; set; }
        public int? Anio { get; set; }
        public int? Mes { get; set; }
        public string? Estatus { get; set; }
        public string? Flujo { get; set; }
        public string? PersonaNombre { get; set; }
        public int? Total { get; set; }
        public int? TotalGeneral { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdFlujo { get; set; }
        public string? TiempoAtencion { get; set; }
        public string? Titulo { get; set; }
        public string? FechaRegistro { get; set; }
    }

}

