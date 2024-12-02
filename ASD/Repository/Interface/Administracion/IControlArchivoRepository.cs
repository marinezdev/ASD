using ASD.Areas.Administracion.Models;

namespace ASD.Repository.Interface.Administracion
{
    public interface IControlArchivoRepository
    {
        Task<ControlArchivoViewModel> CreateControlArchivo();
    }
}
