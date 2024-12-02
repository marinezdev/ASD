using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketCuadrillaRepository
    {
        Task<TicketCuadrillaViewModel> CreateTicketCuadrilla(TicketCuadrillaViewModel ticketCuadrilla);
        Task<TicketCuadrillaViewModel> UpdateTicketCuadrilla(TicketCuadrillaViewModel ticketCuadrilla);
        Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_ProcesarEtapa(TicketCuadrillaViewModel ticketCuadrilla);
        Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_AsignarEtapa(TicketCuadrillaViewModel ticketCuadrilla);
        Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_AtenderTicket(TicketCuadrillaViewModel ticketCuadrilla);
        Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_ValidaAtencion(TicketCuadrillaViewModel ticketCuadrilla);
        Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_FinalizarTicket(TicketCuadrillaViewModel ticketCuadrilla);
        Task<CTicketCuadrillaViewModel> GetTicketCuadrilla_Seleccionar_IdTicket(TicketViewModel ticket);
    }
}
