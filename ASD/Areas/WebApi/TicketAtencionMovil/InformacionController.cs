using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Areas.WebApi.Ticket.Models;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.TicketAtencionMovil
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionController : Controller
    {
        private readonly ILogger<InformacionController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;

        public InformacionController(ILogger<InformacionController> logger, ITicketRepository ticketRepository, ITicketEquipoRepository ticketEquipoRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
        }



        [HttpPost]
        [Route("InformacionTicket")]
        public async Task<dynamic> InformacionTicket([FromBody] TicketViewModel ticket)
        {
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);

            DTicketViewModel Result = new DTicketViewModel();
            Result.Ticket = db_ticket;
            Result.TicketEquipos = dbEquipos;
            return (Result);

        }
    }
}