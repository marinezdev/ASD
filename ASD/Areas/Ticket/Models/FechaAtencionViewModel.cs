namespace ASD.Areas.Ticket.Models
{
    public class FechaAtencionViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public Cat_EstatusFechaAtencionViewModel? Cat_EstatusFechaAtencion { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public DateTime? FechaTermino { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
