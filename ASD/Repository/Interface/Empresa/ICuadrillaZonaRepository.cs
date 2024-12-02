using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICuadrillaZonaRepository
    {
        Task<List<CuadrillaZonaViewModel>> Get_Cuadrilla(CuadrillaZonaViewModel CuadrillaZona);


        Task<CuadrillaZonaViewModel> Delete_CuadrillaZona(CuadrillaZonaViewModel CuadrillaZona);
        Task<CuadrillaZonaViewModel> Add_CuadrillaZona(CuadrillaZonaViewModel CuadrillaZona);

    }
}
