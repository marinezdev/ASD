using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketEquipoImagenRepository
    {
        Task<List<TicketEquipoImagenViewModel>> GetTicketEquipoImagen_Seleccionar_IdTicketEquipo(TicketEquipoViewModel ticketEquipo);
        Task<TicketEquipoImagenViewModel> UpdateTicketEquipoImagen_Actualizar(TicketEquipoImagenViewModel ticketEquipoImagen);
        Task<TicketEquipoImagenViewModel> GetTicketEquipoImagen_Seleccionar_Id(TicketEquipoImagenViewModel ticketEquipoImagen);
        Task<TicketEquipoImagenViewModel> UpdateTicketEquipoImagen_Actualizar_ImagenVS(TicketEquipoImagenViewModel ticketEquipoImagen);
    }
}
