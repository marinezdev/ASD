namespace ASD.Areas.Ticket.Models
{
    public class TicketTipoServicioViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public Cat_TipoServicioViewModel? Cat_TipoServicio { get; set; }
        public string? Valor { get; set; }
        public string? Observaciones { get; set; }
    }
}
