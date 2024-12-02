using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_EstatusOrdenTrabajoRepository
    {
        Task<List<Cat_EstatusOrdenTrabajoViewModel>> GetCat_EstatusOrdenTrabajo_Seleccionar();

        Task<Cat_EstatusOrdenTrabajoViewModel> InsertEstatusOrdenTrabajo(Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo);
        Task<Cat_EstatusOrdenTrabajoViewModel> UpdateEstatusOrdenTrabajo(Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo);
        Task<Cat_EstatusOrdenTrabajoViewModel> DeleteEstatusOrdenTrabajo(Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo);
    }
}
