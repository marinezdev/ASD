using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface ICat_TipoEquipoImagenRepository
    {
        Task<List<Cat_TipoEquipoImagenViewModel>> GetCat_TipoEquipoImagen(Cat_TipoEquipoViewModel cat_TipoEquipo);
    }
}
