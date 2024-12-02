using ASD.Areas.Operacion.Models;

namespace ASD.Repository.Interface.Operacion
{
    public interface IFlujoRepository
    {
        Task<List<FlujoViewModel>> GetFlujo_Seleccionar();
        Task<List<FlujoViewModel>> GetCat_Flujo_Listar_IdUsuario(FlujoViewModel Flujo);

        Task<List<FlujoViewModel>> GetCat_Flujo_Listar_Faltante(UsuarioFlujoViewModel Flujo);
        Task<UsuarioFlujoViewModel> Add_UsuarioFlujo(UsuarioFlujoViewModel UsuarioFlujo);

    }
}
