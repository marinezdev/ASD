using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.TicketAtencionMovil
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovilController : Controller
    {
        private readonly ITicketCuadrillaRepository _ticketCuadrillaRepository;
        private readonly ITicketEtapaRepository _ticketEtapaRepository;


        public MovilController(ITicketCuadrillaRepository ticketCuadrillaRepository, ITicketEtapaRepository ticketEtapaRepository)
        {
            _ticketCuadrillaRepository = ticketCuadrillaRepository;
            _ticketEtapaRepository = ticketEtapaRepository;

        }


        [HttpPost]
        [Route("VerificarTicket")]
        public async Task<dynamic> VerificarTicket([FromBody] TicketCuadrillaViewModel TicketCuadrilla)
        {
            UsuarioViewModel usuario = TicketCuadrilla.Usuario;
            TicketViewModel ticket = TicketCuadrilla.Ticket;

            TicketCuadrillaViewModel dbticketCuadrilla = await _ticketCuadrillaRepository.CreateTicketCuadrilla_ValidaAtencion(TicketCuadrilla);

            if (dbticketCuadrilla.Id > 0)
            {
                return dbticketCuadrilla;

            }
            else
            {
                
                CTicketEtapaViewModel dbTicket = await _ticketEtapaRepository.GetTicketEtapa_Seleccionar_IdTicket(ticket);
                dbTicket.Usuario = usuario;
                return dbTicket;
            }

        }
       
    }
}
