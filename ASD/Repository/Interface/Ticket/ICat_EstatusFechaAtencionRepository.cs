using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_EstatusFechaAtencionRepository
    {
        Task<List<Cat_EstatusFechaAtencionViewModel>> GetCat_EstatusFechaAtencion();


        Task<Cat_EstatusFechaAtencionViewModel> InsertEstatusFechaAtencion(Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion);
        Task<Cat_EstatusFechaAtencionViewModel> UpdateEstatusFechaAtencion(Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion);
        Task<Cat_EstatusFechaAtencionViewModel> DeleteEstatusFechaAtencion(Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion);
    }
}
