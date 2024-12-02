using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;

namespace ASD.Repository.Interface.Dhasbord
{
    public interface IDPrioridadRepository
    {
        Task<List<DPrioridadViewModel>> GetDhasbord_Prioridad_Operacion(UsuarioViewModel usuario);
        Task<List<DPrioridadViewModel>> GetCliente_Ticket_Prioridad(UsuarioViewModel usuario);
        Task<List<DPrioridadViewModel>> GetDhasbord_Prioridad_Operacion();
    }
}
