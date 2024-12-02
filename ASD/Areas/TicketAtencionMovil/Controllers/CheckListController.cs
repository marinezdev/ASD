using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.TicketAtencionMovil.Controllers
{
    [Authorize]
    [Area("TicketAtencionMovil")]
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

        [HttpGet()]
        public virtual async Task<IActionResult> Index([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);
            List<CTicketEquipoViewModel> dbRutinaImg = new List<CTicketEquipoViewModel>();

            if (dbEquipos != null)
            {
                foreach (CTicketEquipoViewModel cTicketEquipo in dbEquipos)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Id = cTicketEquipo.Id;
                    cTicketEquipo.ticketEquipoRutina = await _ticketEquipoRutinaRepository.GetTicketEquipoRutina_Seleccionar_IdTicketEquipo(ticketEquipo);

                    dbRutinaImg.Add(cTicketEquipo);
                }
            }

            ViewBag.Ticket = db_ticket;
            ViewBag.RutinaImg = dbRutinaImg;

            return View();
        }

    }
}
