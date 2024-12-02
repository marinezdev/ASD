using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketEscalacionController : Controller
    {
        private readonly ILogger<TicketEscalacionController> _logger;
        private readonly ITicketEscalacionRepository _ticketEscalacionRepository;

        public TicketEscalacionController(ILogger<TicketEscalacionController> logger, ITicketEscalacionRepository ticketEscalacionRepository)
        {
            _logger = logger;
            _ticketEscalacionRepository = ticketEscalacionRepository;
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicketEscalacion([FromBody] TicketEscalacionViewModel ticketEscalacion)
        {
            TicketEscalacionViewModel db_TicketEscalacion = await _ticketEscalacionRepository.CreateTicketEscalacion(ticketEscalacion);

            return Json(db_TicketEscalacion);
        }

        [HttpPost]
        public async Task<JsonResult> GetTicketEscalacion_Seleccionar_IdTicket([FromBody] TicketEscalacionViewModel escalacionTiempo)
        {
            List<CTicketEscalacionViewModel> db_TicketEscalacion = await _ticketEscalacionRepository.GetTicketEscalacion_Seleccionar_IdTicket(escalacionTiempo);
            return Json(db_TicketEscalacion);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTicketEscalacion([FromBody] TicketEscalacionViewModel escalacionTiempo)
        {
            TicketEscalacionViewModel db_TicketEscalacion = await _ticketEscalacionRepository.DeleteTicketEscalacion(escalacionTiempo);
            return Json(db_TicketEscalacion);
        }
    }
}
