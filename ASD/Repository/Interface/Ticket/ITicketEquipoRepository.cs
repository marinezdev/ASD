using ASD.Areas.Inventario.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketEquipoRepository
    {
        Task<TicketEquipoViewModel> CreateTicketEquipo(TicketEquipoViewModel ticketEquipo);
        Task<List<CTicketEquipoViewModel>> GetTicketEquipo_Seleccionar_IdTicket(TicketViewModel ticket);



        Task<EquipoViewModel> TicketEquipo_CrearSD(EquipoViewModel ticketEquipo);

    }
}
