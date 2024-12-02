using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface ICat_CategoriaRepository
    {
        Task<List<Cat_CategoriaViewModel>> GetCat_Categoria_Seleccionar_IdTipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo);
        Task<Cat_CategoriaViewModel> CreateCat_Categoria_Crear(Cat_CategoriaViewModel cat_Categoria);


        Task<Cat_CategoriaViewModel> Delete_Categoria(Cat_CategoriaViewModel cat_Categoria);
        Task<Cat_CategoriaViewModel> Update_Categoria(Cat_CategoriaViewModel cat_Categoria);
    }
}
