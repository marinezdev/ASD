using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Areas.WebApi.Ticket.Models
{
    public class TicketUnitarioViewModel
    {
        public List<FlujoViewModel>? ListFlujo { get; set; }
        public List<EquipoViewModel>? ListEquipo { get; set; }
    }
}
