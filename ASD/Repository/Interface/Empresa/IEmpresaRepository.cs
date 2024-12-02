using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface IEmpresaRepository
    {
        Task<List<EmpresaViewModel>> GetEmpresa_Seleccionar();

    }
}
