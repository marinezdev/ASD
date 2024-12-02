using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface IBitacoraRepository
    {
        Task<List<CBitacoraViewModel>> GetBitacora_Seleccionar_IdTicket(TicketViewModel ticket);
        Task<CBitacoraViewModel> GetBitacora_UltimoMovimiento_IdTicket(TicketViewModel ticket);
    }
}
