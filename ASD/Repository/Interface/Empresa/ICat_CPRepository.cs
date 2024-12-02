using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICat_CPRepository
    {
        Task<List<Cat_CPViewModel>> GetCat_CP_Seleccionar_IdPoblacion(Cat_PoblacionViewModel cat_Poblacion);
        Task<Cat_CPViewModel> CreateCat_CP(Cat_CPViewModel cat_CP);
    }
}
