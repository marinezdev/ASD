using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class NuevaFechaAtencionViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public string? Observaciones { get; set; }
        public int Validada { get; set; }
        public int Notificar { get; set; }
    }
}
