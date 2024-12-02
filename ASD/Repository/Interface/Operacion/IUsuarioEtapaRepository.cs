using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Repository.Interface.Operacion
{
    public interface IUsuarioEtapaRepository
    {
        Task<List<UsuarioEtapaViewModel>> Etapa_Listar_IdUsuario(UsuarioEtapaViewModel Etapa);
        Task<UsuarioEtapaViewModel> Delete_Etapa(UsuarioEtapaViewModel Etapa);
        Task<UsuarioEtapaViewModel> Add_UsuarioEtapa(UsuarioEtapaViewModel Etapa);

        Task<List<UsuarioEtapaViewModel>> GetCat_Etapa_Listar_Faltante(UsuarioFlujoViewModel Etapa);


    }
}
