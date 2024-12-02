using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.TicketAtencionMovil.Controllers
{
    [Authorize]
    [Area("TicketAtencionMovil")]
    public class FotoAntesController : Controller
    {
        private readonly ILogger<FotoAntesController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;
        private readonly ITicketEquipoImagenRepository _ticketEquipoImagenRepository;

        public FotoAntesController(ILogger<FotoAntesController> logger, ITicketRepository ticketRepository, ITicketEquipoRepository ticketEquipoRepository, ITicketEquipoImagenRepository ticketEquipoImagenRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
            _ticketEquipoImagenRepository = ticketEquipoImagenRepository;
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Index([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            
            ViewBag.Ticket = db_ticket;
            return View();
        }
    }
}
