namespace ASD.Areas.Ticket.Models
{
    public class FolioViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public string? Folio { get; set; }
    }
}
