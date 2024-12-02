using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ISucursalImgRepository
    {
        Task<SucursalImgViewModel> CreateSucursalImg(SucursalImgViewModel sucursalImg);
        Task<SucursalImgViewModel> GetSucursalImg_Seleccionar_Id(SucursalImgViewModel sucursalImg);
        Task<List<SucursalImgViewModel>> GetSucursalImg_Seleccionar_IdTikcet(TicketViewModel ticket);
    }
}
