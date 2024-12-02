namespace ASD.Areas.Ticket.Models
{
    public class TicketAsignacionEmpresaViewModel
    {
        public int Id { get; set; }
        public TicketViewModel? Ticket { get; set; }
        public Cat_AsignacionEmpresaViewModel? Cat_AsignacionEmpresa { get; set; }
    }
}
