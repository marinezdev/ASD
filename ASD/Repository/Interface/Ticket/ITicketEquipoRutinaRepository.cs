using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketEquipoRutinaRepository
    {
        Task<List<TicketEquipoRutinaViewModel>> GetTicketEquipoRutina_Seleccionar_IdTicketEquipo(TicketEquipoViewModel ticketEquipo);
        Task<TicketEquipoRutinaViewModel> UpdateTicketEquipoRutina_Actualizar(TicketEquipoRutinaViewModel ticketEquipoRutina);
    }
}
