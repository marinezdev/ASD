using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface ICat_TipoEquipoRutinaRepository
    {
        Task<List<Cat_TipoEquipoRutinaViewModel>> GetCat_TipoEquipoRutina(Cat_TipoEquipoViewModel cat_TipoEquipo);
    }
}
