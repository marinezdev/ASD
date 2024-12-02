using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketEquipoRutinaController : Controller
    {
        private readonly ILogger<TicketEquipoRutinaController> _logger;
        private readonly ITicketEquipoRutinaRepository _ticketEquipoRutinaRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketEquipoRutinaController(ILogger<TicketEquipoRutinaController> logger, ITicketEquipoRutinaRepository ticketEquipoRutinaRepository, ITicketRepository ticketRepository, IControlArchivoRepository controlArchivoRepository)
        {
            _logger = logger;
            _ticketEquipoRutinaRepository = ticketEquipoRutinaRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpPost]
        [Route("ActualizarRutina")]
        public async Task<dynamic> ActualizarTicketEquipoRutina([FromBody] TicketEquipoRutinaViewModel ticketEquipoRutina)
        {
            // Actualiza la rutina del equipo usando el repositorio
            TicketEquipoRutinaViewModel dbTicketEquipoRutina = await _ticketEquipoRutinaRepository.UpdateTicketEquipoRutina_Actualizar(ticketEquipoRutina);

            // Devuelve el resultado en formato JSON
            return dbTicketEquipoRutina;
        }

    }
}