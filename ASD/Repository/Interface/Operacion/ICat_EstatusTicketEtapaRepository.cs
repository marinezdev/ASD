using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Operacion
{
    public interface ICat_EstatusTicketEtapaRepository
    {
        Task<List<Cat_EstatusTicketEtapaViewModel>> GetCat_TipoEstatusEtapa();
        Task<Cat_EstatusTicketEtapaViewModel> InsertTipoEstatusEtapa(Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa);
        Task<Cat_EstatusTicketEtapaViewModel> DeleteTipoEstatusEtapa(Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa);

        Task<Cat_EstatusTicketEtapaViewModel> UpdateTipoEstatusEtapa(Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa);

    }
}
