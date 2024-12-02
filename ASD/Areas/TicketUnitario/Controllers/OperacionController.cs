using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.TicketUnitario.Controllers
{
    [Authorize]
    [Area("TicketUnitario")]
    public class OperacionController : Controller
    {
        private readonly ILogger<OperacionController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ICat_EstatusTicketRepository _cat_EstatusTicketRepository;
        private readonly ICat_PrioridadRepository _cat_PrioridadRepository;
        private readonly ICat_AsignacionEmpresaRepository _cat_AsignacionEmpresaRepository;

        public OperacionController(ILogger<OperacionController> logger, ITicketRepository ticketRepository, ICat_EstatusTicketRepository cat_EstatusTicketRepository, ICat_PrioridadRepository cat_PrioridadRepository, ICat_AsignacionEmpresaRepository cat_AsignacionEmpresaRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _cat_EstatusTicketRepository = cat_EstatusTicketRepository;
            _cat_PrioridadRepository = cat_PrioridadRepository;
            _cat_AsignacionEmpresaRepository = cat_AsignacionEmpresaRepository;
        }

        public virtual async Task<IActionResult> Index()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicketUnitario_Selecionar_EnOperacion(usuario);
            List<Cat_EstatusTicketViewModel> db_CatEstatusTicket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_PrioridadRepository.GetCat_Prioridad_Seleccionar();
            List<Cat_AsignacionEmpresaViewModel> db_CatAsignacionEmpresa = await _cat_AsignacionEmpresaRepository.GetCat_AsignacionEmpresa_Seleccionar();

            ViewBag.Tickets = db_tickets;
            ViewBag.CatEstatusTicket = db_CatEstatusTicket;
            ViewBag.CatPrioridad = db_CatPrioridad;
            ViewBag.CatAsigacionEmpresa = db_CatAsignacionEmpresa;

            return View();
        }
    }
}
