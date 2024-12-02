using ASD.Areas.Empresa.Models;

namespace ASD.Repository.Interface.Empresa
{
    public interface ICuadrillaUsuarioRepository
    {
        Task<List<CuadrillaUsuarioViewModel>> GetCuadrillaUsuario_Seleccionar_IdCuadrilla(CuadrillaViewModel cuadrilla);
    }
}
