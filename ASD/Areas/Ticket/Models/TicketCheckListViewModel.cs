using ASD.Areas.Inventario.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketCheckListViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public TicketEquipoViewModel? TicketEquipo { get; set;}
        public Cat_EquipoRutinaViewModel? Cat_RutinaEquipo { get; set; }
        public int Estatus { get; set; }
        public string? Observaciones { get; set; }
    }
}
