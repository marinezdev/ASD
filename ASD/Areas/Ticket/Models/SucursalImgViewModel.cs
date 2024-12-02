using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class SucursalImgViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public Cat_SucursalImagenViewModel? Cat_SucursalImagen { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public string? NmOriginal { get; set; }
        public string? NmArchivo { get; set; }
    }
}
