using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICat_TipoSucursalRepository
    {
        Task<List<Cat_TipoSucursalViewModel>> GetCat_TipoSucursal_Seleccionar();
    }
}
