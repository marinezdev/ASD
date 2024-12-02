using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.WebApi.Ticket.Models
{
    public class FullTicketViewModel
    {
        public CreateTicketViewModel? CreateTicket { get; set; }
        public List<ArchivoViewModel>? Archivos { get; set; }
    }
}
