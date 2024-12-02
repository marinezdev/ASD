using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICat_ColoniaRepository
    {
        Task<List<Cat_ColoniaViewModel>> GetCat_Colonia_Seleccionar_IdCP(Cat_CPViewModel cat_CP);
        Task<Cat_ColoniaViewModel> CreateCat_Colonia(Cat_ColoniaViewModel cat_Colonia);
    }
}
