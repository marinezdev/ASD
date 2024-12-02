using ASD.Areas.Administracion.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Areas.Ticket.Models
{
    public class EscalacionViewModel
    {
        public int Id { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public FlujoViewModel? Flujo { get; set; }
        public int Dias { get; set; }
    }
}
