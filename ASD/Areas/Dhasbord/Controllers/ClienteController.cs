using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.Dhasbord.Controllers
{
    [Authorize]
    [Area("Dhasbord")]
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IDTicketRepository _dticketRepositoy;
        private readonly IDEstatusTicketRepository _DEstatusTicketRepository;
        private readonly IDPrioridadRepository _DprioridadRepository;

        public ClienteController(ILogger<ClienteController> logger, IDTicketRepository dticketRepository, IDEstatusTicketRepository dEstatusTicketRepository, IDPrioridadRepository dprioridadRepository)
        {
            _logger = logger;
            _dticketRepositoy = dticketRepository;
            _DEstatusTicketRepository = dEstatusTicketRepository;
            _DprioridadRepository = dprioridadRepository;
        }

        public virtual async Task<IActionResult> Index()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;
            string nombreUsuario = "";

            if (claimuser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<DTicketViewModel> dTicketsFlujo = await _dticketRepositoy.GetCliente_Ticket_Flujo(usuario);
            List<DTicketViewModel> dbTicketsEstatus = await _DEstatusTicketRepository.GetCliente_Ticket_EstatusTicket(usuario);
            List<DPrioridadViewModel> dbTicketsPrioridad =  await _DprioridadRepository.GetCliente_Ticket_Prioridad(usuario);

            ViewBag.Nombre = nombreUsuario;
            ViewBag.TicketFlujo = dTicketsFlujo;
            ViewBag.TicketEstatus  = dbTicketsEstatus;
            ViewBag.TicketProridad = dbTicketsPrioridad;

            return View();
        }

    }
}
