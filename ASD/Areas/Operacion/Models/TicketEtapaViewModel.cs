using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.Operacion.Models
{
    public class TicketEtapaViewModel
    {
        public TicketViewModel? Ticket { get; set; }
        public EtapaViewModel? Etapa { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public Cat_EstatusTicketEtapaViewModel? Cat_EstatusTicketEtapa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
