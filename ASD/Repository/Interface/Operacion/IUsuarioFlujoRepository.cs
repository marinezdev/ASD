using ASD.Areas.Persona.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Operacion
{
    public interface IUsuarioFlujoRepository
    {
        Task<List<EmailViewModel>> GetUsuarioEtapa_Notificacion_Operacion(TicketViewModel ticket, string Etapa);
    }
}
