using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface ICat_EstatusEquipoRepository
    {
        Task<List<Cat_EstatusEquipoViewModel>> GetCat_EstatusEquipo_Seleccionar();
    }
}
