using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface IEquipoRepository
    {
        Task<List<EquipoViewModel>> GetEquipo_Seleccionar_IdSucursal(SucursalViewModel sucursal);
        Task<EquipoViewModel> CreateEquipo(EquipoViewModel equipo);
    }
}
