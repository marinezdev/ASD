using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface INuevaFechaAtencionRepository
    {
        Task<NuevaFechaAtencionViewModel> CreateNuevaFechaAtencion_Crear(NuevaFechaAtencionViewModel nuevaFechaAtencion);
        Task<NuevaFechaAtencionViewModel> CreateNuevaFechaAtencion_Crear_Super(NuevaFechaAtencionViewModel nuevaFechaAtencion);
        Task<NuevaFechaAtencionViewModel> UpdateNuevaFechaAtencion_Aceptar(NuevaFechaAtencionViewModel nuevaFechaAtencion);
        Task<CNuevaFechaAtencionViewModel> GetNuevaFechaAtencion_Seleccionar_IdTicket(TicketViewModel ticket);

        Task<NuevaFechaAtencionViewModel> CreateNuevaFechaAtencion_Crear_TicketUnitario(NuevaFechaAtencionViewModel nuevaFechaAtencion);
    }
}
