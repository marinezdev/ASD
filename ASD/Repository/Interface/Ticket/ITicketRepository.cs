using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Areas.TicketUnitario.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ITicketRepository
    {
        Task<List<CTicketViewModel>> GetTicket_Seleccionar_IdUsuario(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_EnOperacion(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_OperacionOrdenTrabajo(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_ValidadoCliente(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_ValidadoSupervisor(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_AtendidosOperador(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_EnReasignacion(UsuarioViewModel usuario);
        Task<List<CTicketViewModel>> GetTicket_Tickets_Seleccionar_Estado(UsuarioViewModel usuario, Cat_EstadoViewModel cat_Estado);
        Task<CTicketViewModel> GetTicket_Seleccionar_Id(TicketViewModel ticket);
        Task<CreateTicketViewModel> CreateTicket(CreateTicketViewModel createTicket);
        Task<CreateTicketViewModel> CreateTicket_Crear_Usuario(CreateTicketViewModel ticket);
        Task <List<CTicketViewModel>> GetTicket_Seleccionar_IdEstatus(TicketViewModel ticket);
        Task<TicketViewModel> UpdateTicket_ActualizarEstatus(TicketViewModel ticket);
        Task<List<CTicketViewModel>> GetTicket_Selecionar_EnOperacion_Usuario(UsuarioViewModel usuario);
        Task<TicketViewModel> UpdateTicket_Procesar_SupervisorAsae(TicketViewModel ticket);
        Task<TicketViewModel> UpdateTicket_Procesar_SupervisorCliente(TicketViewModel ticket);
        Task<TicketViewModel> UpdateTicketProcesar_Cliente(TicketViewModel ticket);
        Task<TicketViewModel> UpdateTicket(TicketViewModel ticket);
        Task<TicketViewModel> UpdateTicket_Actualizar_IdPrioridad(TicketViewModel ticket);
        Task<List<CTicketViewModel>> GetTicketUnitario_Selecionar_EnOperacion(UsuarioViewModel usuario);
        Task<TicketAtenderViewModel> Ticket_Procesar_Atendido(TicketAtenderViewModel ticket);
        Task<CTicketViewModel> GetTicket_Seleccionar_Id(TicketAtenderViewModel ticket);
        Task<TicketAtenderViewModel> Ticket_Procesar_Cierre(TicketAtenderViewModel ticket);
        Task<TicketAtenderViewModel> Ticket_Procesar_NoAtendido(TicketAtenderViewModel ticket);

        Task<List<TicketReporteViewmodel>> ObtenerTicketsPorMesYAnio();

        Task<List<TicketReporteViewmodel>> ObtenerTotalTicketsPorEstatusAnio(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> ObtenerTotalTicketsPorFlujoAnio(TicketReporteViewmodel Model);


        Task<List<TicketReporteViewmodel>> ObtenerTicketsAtendidos(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> ObtenerTicketsAnio();

        Task<List<TicketReporteViewmodel>> ObtenerResumenFaltante(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> ObtenerTicketIdflujoUser(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> GetTicketEstatusAnio(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> GetTicketAnioMonth(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> ObtenerTicketXservicio(TicketReporteViewmodel Model);
        Task<List<TicketReporteViewmodel>> ObtenerTicketFinalizado(TicketReporteViewmodel Model);
        Task<TicketReporteViewmodel> TicketFechaCorte(TicketReporteViewmodel Model);

        Task<List<CTicketViewModel>> Ticket_Selecionar_EnOperacionAdmin();

        Task<List<CTicketViewModel>> GetTicket_Tickets_Seleccionar_Estado(Cat_EstadoViewModel cat_Estado);

    }
}
