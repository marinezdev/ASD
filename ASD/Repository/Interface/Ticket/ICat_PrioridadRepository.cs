using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_PrioridadRepository
    {
        Task<List<Cat_PrioridadViewModel>> GetCat_Prioridad_Seleccionar();

        Task<Cat_PrioridadViewModel> InsertPrioridad(Cat_PrioridadViewModel cat_Prioridad);
        Task<Cat_PrioridadViewModel> UpdatePrioridad(Cat_PrioridadViewModel cat_Prioridad);
        Task<Cat_PrioridadViewModel> DeletePrioridad(Cat_PrioridadViewModel cat_Prioridad);
    }
}
