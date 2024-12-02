namespace ASD.Areas.Ticket.Models.Consulta
{
    public class CountTicketViewModel
    {
        public int Id { get; set; }
        public int total { get; set; }
        public TicketViewModel? Ticket { get; set; }
    }
}
