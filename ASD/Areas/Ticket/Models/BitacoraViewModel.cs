using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class BitacoraViewModel
    {
        public TicketViewModel? Ticket { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public Cat_OperacionViewModelcs? Cat_Operacion { get; set;}
        public string? Nota { get; set; }
        public DateTime? FechaRegistro { get; set; }

    }
}
