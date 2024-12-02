using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public FlujoViewModel? Flujo { get; set; }
        public Cat_PrioridadViewModel? Cat_Prioridad { get; set; }
        public Cat_EstatusTicketViewModel? Cat_EstatusTicket { get; set; }
        public SucursalViewModel? Sucursal { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public string? Titulo { get; set; }
        public string? Detalle { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int Notificar { get; set; }
    }
}
