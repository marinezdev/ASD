using ASD.Areas.Inventario.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketEquipoViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public EquipoViewModel? Equipo { get; set; }
        public string? Observaciones { get; set; }

    }
}
