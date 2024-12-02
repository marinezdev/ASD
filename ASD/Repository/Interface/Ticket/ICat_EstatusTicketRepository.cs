using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_EstatusTicketRepository
    {
        Task<List<Cat_EstatusTicketViewModel>> Cat_EstatusTicket_Seleccionar();
        Task<List<CountTicketViewModel>> Cat_EstatusTicket_SeleccionarConteo(UsuarioViewModel usuario);
        Task<Cat_EstatusTicketViewModel> DeleteStatusTicket(Cat_EstatusTicketViewModel Cat_EstatusTicket);
        Task<Cat_EstatusTicketViewModel> UpdateStatusTicket(Cat_EstatusTicketViewModel Cat_EstatusTicket);
        Task<Cat_EstatusTicketViewModel> InsertStatusTicket(Cat_EstatusTicketViewModel Cat_EstatusTicket);
        Task<List<CountTicketViewModel>> Cat_EstatusTicket_SeleccionarConteoAdmin();

    }
}
