using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class ArchivoViewModel
    {
        public int Id { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public string? NmOriginal { get; set; }
        public string? NmArchivo { get; set; }
        public string? Extencion { get; set; }
        public string? Observaciones { get; set; }

    }
}
