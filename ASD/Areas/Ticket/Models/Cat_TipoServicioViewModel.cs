using ASD.Areas.Operacion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class Cat_TipoServicioViewModel
    {
        public int Id { get; set; }
        public FlujoViewModel? Flujo { get; set; }
        public string? Nombre { get; set; }
        public string? TituloMedida { get; set; }
    }
}
