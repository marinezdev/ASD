using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.Dhasbord.Models
{
    public class DTicketViewModel
    {
        public FlujoViewModel? Flujo { get; set; }
        public EtapaViewModel? Etapa { get; set; }
        public Cat_EstatusTicketViewModel? Cat_EstatusTicket { get; set; }
        public int? Total { get; set; }
    }
}
