using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class TicketCuadrillaController : Controller
    {
        private readonly ITicketCuadrillaRepository _ticketCuadrillaRepository;
        public TicketCuadrillaController(ITicketCuadrillaRepository ticketCuadrillaRepository)
        {
            _ticketCuadrillaRepository = ticketCuadrillaRepository;
        }

        [HttpPost]
        [Route("Atender")]
        public async Task<dynamic> VerificarTicket([FromBody] TicketCuadrillaViewModel TicketCuadrilla)
        {
            TicketCuadrillaViewModel db_ticketCuadrilla = await _ticketCuadrillaRepository.CreateTicketCuadrilla_AtenderTicket(TicketCuadrilla);
            return db_ticketCuadrilla;
        }

    }
}