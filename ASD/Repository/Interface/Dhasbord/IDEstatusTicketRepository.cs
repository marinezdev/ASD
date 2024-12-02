using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;

namespace ASD.Repository.Interface.Dhasbord
{
    public interface IDEstatusTicketRepository
    {
        Task<List<DTicketViewModel>> GetCliente_Ticket_EstatusTicket(UsuarioViewModel usuario);
    }
}
