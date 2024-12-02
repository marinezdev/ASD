using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICuadrillaRepository
    {
        Task<List<CuadrillaViewModel>> GetCuadrilla_Seleccionar();
        Task<List<CuadrillaViewModel>> GetCuadrillaIdEmpresa(CuadrillaViewModel Cuadrilla);
        Task<CuadrillaViewModel> CreateCuadrilla(CuadrillaViewModel Cuadrilla);
        Task<CuadrillaViewModel> Delete_Cuadrilla(CuadrillaViewModel Cuadrilla);

    }
}
