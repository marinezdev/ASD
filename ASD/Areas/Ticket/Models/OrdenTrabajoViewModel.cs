using ASD.Areas.Administracion.Models;
using Microsoft.AspNetCore.Html;
using NuGet.Frameworks;

namespace ASD.Areas.Ticket.Models
{
    public class OrdenTrabajoViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public Cat_EstatusOrdenTrabajoViewModel? Cat_EstatusOrdenTrabajo { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public string? NmOriginal { get; set; }
        public string? NmArchivo { get; set; }
        public HtmlString? iframe { get; set; }
        public string? Observaciones { get; set; }
        public int Notificar { get; set; }
    }
}
