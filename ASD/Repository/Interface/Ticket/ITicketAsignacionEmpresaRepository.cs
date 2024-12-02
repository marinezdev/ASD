using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketAsignacionEmpresaRepository
    {
        Task<TicketAsignacionEmpresaViewModel> CreateTicketAsignacionEmpresa(TicketAsignacionEmpresaViewModel ticketAsignacionEmpresa);
    }
}
