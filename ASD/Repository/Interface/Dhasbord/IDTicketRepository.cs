using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Dhasbord
{
    public interface IDTicketRepository
    {
        Task<List<DTicketViewModel>> GetDhasbord_Flujos_TotalTickets(UsuarioViewModel usuario);
        Task<List<DTicketViewModel>> GetCliente_Ticket_Flujo(UsuarioViewModel usuario);
        Task<List<DTicketViewModel>> GetDhasbord_Etapas_Operacion(UsuarioViewModel usuario);
        Task<List<Cat_EstadoViewModel>> GetDhasbord_Mapatikets(UsuarioViewModel usuario);
        Task<List<DTicketViewModel>> GetDhasbord_Flujos_TotalTickets();
        Task<List<DTicketViewModel>> GetDhasbord_Etapas_Operacion();
        Task<List<Cat_EstadoViewModel>> GetDhasbord_Mapatikets();


    }
}
