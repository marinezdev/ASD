using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface IOrdenTrabajoRepository
    {
        Task<OrdenTrabajoViewModel> CreateOrdenTrabajo(OrdenTrabajoViewModel ordenTrabajo);
        Task<OrdenTrabajoViewModel> GetOrdenTrabajo_Seleccionar_IdTicket(TicketViewModel ticket);
        Task<OrdenTrabajoViewModel> UpdateOrdenTrabajo_ActualizarEstatus(OrdenTrabajoViewModel ordenTrabajo);
        Task<OrdenTrabajoViewModel> CreateOrdenTrabajo_ProcesarEtapa(OrdenTrabajoViewModel ordenTrabajo);

        Task<OrdenTrabajoViewModel> Archivo_Seleccionar_Id(ArchivoViewModel archivo);
    }
}
