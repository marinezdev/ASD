using ASD.Areas.Administracion.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_TipoServicioRepository
    {
        Task<List<Cat_TipoServicioViewModel>> GetCat_TipoServicio_Seleccionar_IdFlujo(FlujoViewModel flujo);
        Task<Cat_TipoServicioViewModel> GetCat_TipoServicio_Seleccionar_Id(Cat_TipoServicioViewModel cat_TipoServicio);
    }
}
