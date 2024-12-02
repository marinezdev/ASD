using ASD.Areas.Inventario.Models;

namespace ASD.Areas.Ticket.Models.Consulta
{
    public class CTicketEquipoViewModel
    {
        public int Id { get; set; }
        public EquipoViewModel? Equipo { get; set; }
        public Cat_EstatusEquipoViewModel? Cat_EstatusEquipo { get; set; }
        public Cat_ModeloViewModel? Cat_Modelo { get; set; }
        public Cat_CategoriaViewModel? Cat_Categoria { get; set; }
        public Cat_TipoEquipoViewModel? Cat_TipoEquipo { get; set; }

        public List<TicketEquipoRutinaViewModel>? ticketEquipoRutina { get; set; }
        public List<TicketEquipoImagenViewModel>? ticketEquipoImagen { get; set; }
    }
}
