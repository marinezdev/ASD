using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
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
        public async Task<JsonResult> UpdateTicketEquipoRutina([FromBody] TicketEquipoRutinaViewModel ticketEquipoRutina)
        {
            TicketEquipoRutinaViewModel dbTicketEquipoRutina = await _ticketEquipoRutinaRepository.UpdateTicketEquipoRutina_Actualizar(ticketEquipoRutina);
            return Json(dbTicketEquipoRutina);
        }
    }
}
