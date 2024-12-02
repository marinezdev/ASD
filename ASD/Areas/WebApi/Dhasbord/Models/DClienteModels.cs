using ASD.Areas.Dhasbord.Models;

namespace ASD.Areas.WebApi.Dhasbord.Models
{
    public class DClienteModels
    {
        public List<DTicketViewModel>? TicketsFlujos { get; set; }
        public List<DTicketViewModel>? TicketsEstatus { get; set; }
        public List<DPrioridadViewModel>? TicketsPrioridad { get; set; }
    }
}
