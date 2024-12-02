using ASD.Areas.Administracion.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;

using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Persona;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.TicketAtencionMovil;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.TicketAtencionMovil.Controllers
{
    [Authorize]
    [Area("TicketAtencionMovil")]
    public class CierreClienteController : Controller
    {
        private readonly ILogger<CierreClienteController> _logger;
        private readonly ICierreClienteRepository _CierreClienteRepository;
        private readonly ITicketRepository _ticketRepository;

        public CierreClienteController(ILogger<CierreClienteController> logger,ICierreClienteRepository cierreClienteRepository, ITicketRepository ticketRepository)
        {
            _logger = logger;
            _CierreClienteRepository = cierreClienteRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Index([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);
           

            CierreClienteViewModel CierreCliente = new CierreClienteViewModel();
            CierreCliente.IdTicket = Convert.ToInt32(IdN);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            CierreClienteViewModel R = await _CierreClienteRepository.Get_CierreCliente(CierreCliente);

            ViewBag.Ticket = db_ticket;
            ViewBag.Info = R;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Create_CierreCliente([FromBody] CierreClienteViewModel CierreCliente)
        {
            CierreClienteViewModel Result = await _CierreClienteRepository.Create_CierreCliente(CierreCliente);
            return Json(Result);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_CierreClienteFirma([FromBody] CierreClienteViewModel CierreCliente)
        {
            
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(CierreCliente.Clave));

            CierreCliente.IdTicket = Convert.ToInt32(IdN);

            CierreClienteViewModel Result = await _CierreClienteRepository.Delete_CierreClienteFirma(CierreCliente);
            return Json(Result);
        }
    }
}
