using ASD.Areas.Inventario.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketEquipoRutinaViewModel
    {
        public int Id { get; set; }
        public TicketEquipoViewModel? TicketEquipo { get; set; }
        public Cat_EquipoRutinaViewModel? Cat_EquipoRutina { get; set; }
        public int Estatus { get; set; }
        public string? Observaciones { get; set; }
    }
}
