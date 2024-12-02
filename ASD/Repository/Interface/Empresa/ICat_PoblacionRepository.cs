using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICat_PoblacionRepository
    {
        Task<List<Cat_PoblacionViewModel>> GetCat_Poblacion_Seleccionar_IdEstado(Cat_EstadoViewModel cat_Estado);
        Task<Cat_PoblacionViewModel> CreateCat_Poblacion(Cat_PoblacionViewModel cat_Poblacion);
    }
}
