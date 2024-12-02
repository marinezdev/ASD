using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using System.Threading.Tasks;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketEscalacionRepository
    {
        Task<TicketEscalacionViewModel> CreateTicketEscalacion(TicketEscalacionViewModel ticketEscalacion);
        Task<List<CTicketEscalacionViewModel>> GetTicketEscalacion_Seleccionar_IdTicket(TicketEscalacionViewModel ticketEscalacion);
        Task<TicketEscalacionViewModel> DeleteTicketEscalacion(TicketEscalacionViewModel ticketEscalacion);
    }
}
