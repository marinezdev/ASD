using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Inventario
{
    public interface ICat_ModeloRepository
    {
        Task<List<Cat_ModeloViewModel>> GetCat_Modelo_Seleccionar_IdCategoria(Cat_CategoriaViewModel cat_Categoria);
        Task<Cat_ModeloViewModel> CreateCat_Modelo_Crear(Cat_ModeloViewModel cat_Modelo);

        Task<Cat_ModeloViewModel> Delete_Modelo(Cat_ModeloViewModel cat_Modelo);
        Task<Cat_ModeloViewModel> Update_Modelo(Cat_ModeloViewModel cat_Modelo);
    }
}
