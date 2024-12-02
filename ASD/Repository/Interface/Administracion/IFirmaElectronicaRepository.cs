using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;

namespace ASD.Repository.Interface.Administracion
{
    public interface IFirmaElectronicaRepository
    {
        Task <FirmaElectronicaViewModel> CreateFirma(FirmaElectronicaViewModel firmaElectronica);
        Task<InformacionAdicionalViewModel> Create_Signature(InformacionAdicionalViewModel firmaElectronica);
        Task<InformacionAdicionalViewModel> GetSignature(TicketViewModel ticket);
        Task<InformacionAdicionalViewModel> Delete_Firma(InformacionAdicionalViewModel InformacionAdicional);
        Task<InformacionAdicionalViewModel> UpdateIDC_Firma(InformacionAdicionalViewModel InformacionAdicional);
        Task<InformacionAdicionalViewModel> UpdateCliente_Firma(InformacionAdicionalViewModel InformacionAdicional);

        Task<SignatureTicketViewModel> Ticket_Procesar_Supervisor_Signature(SignatureTicketViewModel ticket);

        
    }
}
