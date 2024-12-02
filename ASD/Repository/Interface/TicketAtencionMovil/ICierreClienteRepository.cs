using ASD.Areas.Inventario.Models;
using ASD.Areas.TicketAtencionMovil.Models;

namespace ASD.Repository.Interface.TicketAtencionMovil
{
    public interface ICierreClienteRepository
    {
        Task<CierreClienteViewModel> Create_CierreCliente(CierreClienteViewModel CierreCliente);

        Task<CierreClienteViewModel> Get_CierreCliente(CierreClienteViewModel CierreCliente);

        Task<CierreClienteViewModel> Delete_CierreClienteFirma(CierreClienteViewModel CierreCliente);
    }
}
