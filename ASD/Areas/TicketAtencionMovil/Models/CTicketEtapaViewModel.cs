using ASD.Areas.Administracion.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Persona.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.TicketAtencionMovil.Models
{
    public class CTicketEtapaViewModel
    {
        public TicketViewModel? Ticket { get; set; }
        public FolioViewModel? Folio { get; set; }
        public EtapaViewModel? Etapa { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public EmailViewModel? Email { get; set; }
        public TelefonoViewModel? Telefono { get; set; }
        public Cat_EstatusTicketEtapaViewModel? Cat_EstatusTicketEtapa { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
