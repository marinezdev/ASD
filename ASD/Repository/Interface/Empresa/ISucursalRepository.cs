using ASD.Areas.Empresa.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ISucursalRepository
    {
        Task<List<SucursalViewModel>> GetSucursal_Seleccionar();
        Task<SucursalViewModel> GetSucursal_Seleccionar_Id(SucursalViewModel sucursal);
        Task<SucursalViewModel> CreateSucursal(SucursalViewModel sucursal);
        Task<List<SucursalViewModel>> GetSucursalIdEmpresa(SucursalViewModel sucursal);
    }
}
