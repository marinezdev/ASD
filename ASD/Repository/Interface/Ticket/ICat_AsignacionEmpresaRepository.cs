using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_AsignacionEmpresaRepository
    {
        Task<List<Cat_AsignacionEmpresaViewModel>> GetCat_AsignacionEmpresa_Seleccionar();

        Task<Cat_AsignacionEmpresaViewModel> InsertAsignacionEmpresa(Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa);
        Task<Cat_AsignacionEmpresaViewModel> UpdateAsignacionEmpresa(Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa);
        Task<Cat_AsignacionEmpresaViewModel> DeleteAsignacionEmpresa(Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa);
    }
}
