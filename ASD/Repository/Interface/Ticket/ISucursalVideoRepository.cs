using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ISucursalVideoRepository
    {
        Task<SucursalVideoViewModel> CreateSucursalVideo(SucursalVideoViewModel sucursaVideo);
        Task<SucursalVideoViewModel> GetSucursalVideo_Seleccionar_Id(SucursalVideoViewModel sucursalVideo);
        Task<List<SucursalVideoViewModel>> GetSucursalVideo_Seleccionar_IdTikcet(TicketViewModel ticket);
    }
}
