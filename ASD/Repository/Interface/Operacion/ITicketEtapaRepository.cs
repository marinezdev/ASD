using ASD.Areas.Operacion.Models;
using ASD.Areas.Operacion.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.TicketAtencionMovil.Models;

namespace ASD.Repository.Interface.Operacion
{
    public interface ITicketEtapaRepository
    {
        Task<List<TicketEtapaViewModel>> GetTicketEtapa_Medidor(TicketViewModel ticket);
        Task<CTicketEtapaViewModel> GetTicketEtapa_Seleccionar_IdTicket(TicketViewModel ticket);
        Task<List<CTicketEtapa2ViewModel>> GetTicketEtapa_Consualta_IdTicket(TicketViewModel ticket);
    }
}
