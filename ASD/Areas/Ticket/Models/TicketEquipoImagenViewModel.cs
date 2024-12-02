using ASD.Areas.Inventario.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketEquipoImagenViewModel
    {
        public int Id { get; set; }
        public TicketEquipoViewModel? TicketEquipo { get; set; }
        public Cat_EquipoImagenViewModel? Cat_EquipoImagen { get; set; }
        public int Estatus { get; set; }
        public string? Imagen { get; set; }
        public string? ImagenVS { get; set; }
        public string? Observaciones { get; set; }
    }
}
