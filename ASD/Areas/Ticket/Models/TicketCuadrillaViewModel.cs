using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketCuadrillaViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public CuadrillaViewModel? Cuadrilla { get; set;}
        public UsuarioViewModel? Usuario { get; set; }
        public string? Observaciones { get; set; }
        public int Notificar { get; set; }
    }
}
