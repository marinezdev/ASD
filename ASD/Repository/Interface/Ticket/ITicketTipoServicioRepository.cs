using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketTipoServicioRepository
    {
        Task<TicketTipoServicioViewModel> CreateTicketTipoServicio(TicketTipoServicioViewModel ticketTipoServicio);
    }
}
