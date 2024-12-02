using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class SucursalVideoViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public Cat_SucursalVideoViewModel? Cat_SucursalVideo { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public string? NmOriginal { get; set; }
        public string? NmArchivo { get; set;}


        public IFormFile? File { get; set; } // Para el archivo de video
    }
}
