using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface IEscalacionTiempoRepository
    {
        Task<EscalacionTiempoViewModel> CreateEscalacionTiempo_Insertar(EscalacionTiempoViewModel escalacionTiempo);
        Task<List<EscalacionTiempoViewModel>> GetEscalacionTiempo_Seleccionar_IdFlujo(EscalacionTiempoViewModel escalacionTiempo);
        Task<EscalacionTiempoViewModel> DeleteEscalacionTiempo_Eliminar(EscalacionTiempoViewModel escalacionTiempo);
    }
}
