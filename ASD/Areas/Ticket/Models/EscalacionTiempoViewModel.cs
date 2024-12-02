using ASD.Areas.Operacion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class EscalacionTiempoViewModel
    {
        public int Id { get; set; }
        public FlujoViewModel? Flujo { get; set; }
        public string? Hora { get; set; } 

    }
}
