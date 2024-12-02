using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Operacion.Models.Consulta;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;


namespace ASD.Areas.WebApi.Ticket.Models
{
    public class DTicketViewModel
    {
        public CTicketViewModel? Ticket { get; set; }
        public List<CTicketEquipoViewModel>? TicketEquipos { get; set;  }
        public List<CBitacoraViewModel>? TicketBitacoras { get; set; }
        public List<TicketEtapaViewModel>? TicketEtapas { get; set; }
        public List<ArchivoViewModel>? TicketArchivos { get; set; }
        public List<CTicketEtapa2ViewModel>? TicketEtapasTiempo  { get; set; }
        public List<DTiempoAtencionViewModel>? TicketTiempoAtencion { get; set; }
    }
}
