using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface IArchivoRepository
    {
        Task<ArchivoViewModel> CreateArchivo(ArchivoViewModel archivo);
        Task<List<ArchivoViewModel>> GetArchivo_Seleccionar_IdTicket(TicketViewModel ticket);
        Task<ArchivoViewModel> DeleteArchivo(ArchivoViewModel archivo);
    }
}
