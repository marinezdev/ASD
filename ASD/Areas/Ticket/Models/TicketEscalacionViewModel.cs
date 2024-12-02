using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketEscalacionViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public int Dias { get; set; }
    }
}
