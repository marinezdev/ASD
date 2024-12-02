using ASD.Areas.Inventario.Models;

namespace ASD.Areas.Ticket.Models.Consulta
{
    public class CreateTicketViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public TicketTipoServicioViewModel? TicketTipoServicio { get; set; }
        public FolioViewModel? Folio { get; set; }
        public FechaAtencionViewModel? FechaAtencion { get; set; }
        public TicketEquipoViewModel? TicketEquipo { get; set; }


        // Variables adicionales para captura por usuario
        public int Archivo { get; set; }
        public int Equipo { get; set; }
    }
}
