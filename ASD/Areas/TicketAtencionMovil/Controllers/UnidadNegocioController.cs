using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencion.Controllers;
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
    public class UnidadNegocioController : Controller
    {
        private readonly ILogger<UnidadNegocioController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISucursalVideoRepository _sucursalVideoRepository;
        private readonly ISucursalImgRepository _sucursalImgRepository;

        public UnidadNegocioController(ILogger<UnidadNegocioController> logger, ITicketRepository ticketRepository, ISucursalVideoRepository sucursalVideoRepository, ISucursalImgRepository sucursalImgRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _sucursalVideoRepository = sucursalVideoRepository;
            _sucursalImgRepository = sucursalImgRepository;
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
