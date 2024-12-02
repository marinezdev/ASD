using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICat_EstadoRepository
    {
        Task<List<Cat_EstadoViewModel>> GetCat_Estado_Seleccionar();
        Task<Cat_EstadoViewModel> CreateCat_Estado(Cat_EstadoViewModel cat_Estado);
    }
}
 