using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
    public interface IEscalacionRepository
    {
        Task<EscalacionViewModel> CreateEscalacion_Insertar(EscalacionViewModel escalacion);
        Task<List<CEscalacionViewModel>> GetEscalacion_Seleccionar_IdFlujo(EscalacionViewModel escalacion);
        Task<EscalacionViewModel> DeleteEscalacion_Eliminar(EscalacionViewModel escalacion);
    }
}
