using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Dhasbord
{
    public interface IDTiempoAtencionRepository
    {
        Task<List<DTiempoAtencionViewModel>> GetDhasbord_Tiempos_Atencion(UsuarioViewModel usuario);
        Task<List<DTiempoAtencionViewModel>> GetDhasbord_Tiempos_Atencion_Ticket(TicketViewModel ticket);
        Task<List<CTicketViewModel>> GetDhasbord_Tiempos_Atencion_Tickets(UsuarioViewModel usuario, EtapaViewModel etapa);
        Task<List<DTiempoAtencionViewModel>> GetDhasbord_Tiempos_Atencion();

    }
}
