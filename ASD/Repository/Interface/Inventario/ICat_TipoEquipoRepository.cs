using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface ICat_TipoEquipoRepository
    {
        Task<List<Cat_TipoEquipoViewModel>> GetCat_TipoEquipo_Seleccionar();
        Task<Cat_TipoEquipoViewModel> CreateCat_TipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo);
        Task<Cat_TipoEquipoViewModel> Delete_TipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo);
        Task<Cat_TipoEquipoViewModel> Update_TipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo);

    }
}
