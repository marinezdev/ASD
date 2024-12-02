using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;

namespace ASD.Repository.Interface.Dhasbord
{
    public interface IDPendientesRepository
    {
        Task<DPendientesViewModel> GetDhasbord_Pendientes_IdUsuario(UsuarioViewModel usuario);
        Task<DPendientesViewModel> GetDhasbord_Pendientes_IdUsuario();

    }
}
