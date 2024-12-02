using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.TicketAtencionMovil
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckListController : Controller
    {
        private readonly ILogger<CheckListController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;
        private readonly ITicketEquipoRutinaRepository _ticketEquipoRutinaRepository;
        public CheckListController(ILogger<CheckListController> logger, ITicketRepository ticketRepository, ITicketEquipoRepository ticketEquipoRepository, ITicketEquipoRutinaRepository ticketEquipoRutinaRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
            _ticketEquipoRutinaRepository = ticketEquipoRutinaRepository;
        }


        [HttpPost]
        [Route("ChecksEquipos")]
        public async Task<dynamic> InformacionCompletaTicket([FromBody] TicketViewModel ticket)
        {
            // Obtiene el ticket usando el repositorio
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            // Obtiene los equipos relacionados con el ticket
            List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);

            // Lista para almacenar los equipos con sus rutinas
            List<CTicketEquipoViewModel> dbRutinaImg = new List<CTicketEquipoViewModel>();

            // Si existen equipos relacionados, procesa cada uno
            if (dbEquipos != null)
            {
                foreach (CTicketEquipoViewModel cTicketEquipo in dbEquipos)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Id = cTicketEquipo.Id;

                    // Obtiene las rutinas relacionadas con el equipo y las agrega a la propiedad
                    cTicketEquipo.ticketEquipoRutina = await _ticketEquipoRutinaRepository.GetTicketEquipoRutina_Seleccionar_IdTicketEquipo(ticketEquipo);

                    dbRutinaImg.Add(cTicketEquipo);
                }
            }

            // Crea un objeto de respuesta que incluya el ticket y los equipos con rutinas
            var result = new
            {
                Ticket = db_ticket,
                RutinaImg = dbRutinaImg
            };

            // Devuelve el resultado en formato JSON
            return result;
        }


    }
}