using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ASD.Areas.Dhasbord.Controllers
{
    [Authorize]
    [Area("Dhasbord")]
    public class SuperNoOrdenTrabajoController : Controller
    {
        private readonly ILogger<SuperNoOrdenTrabajoController> _logger;
        private readonly IDPrioridadRepository _dprioridadRepository;
        private readonly IDPendientesRepository _dPendientesRepository;
        private readonly IDTiempoAtencionRepository _dTiempoAtencionRepository;
        private readonly IDFirmaRecibidoRepository _dfirmaRecibidoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ICat_EstatusTicketRepository _cat_EstatusTicketRepository;
        private readonly IDTicketRepository _dticketRepositoy;

        public SuperNoOrdenTrabajoController(ILogger<SuperNoOrdenTrabajoController> logger, IDPrioridadRepository dPrioridadRepository, IDPendientesRepository dPendientesRepository, ITicketRepository ticketRepository, ICat_EstatusTicketRepository cat_EstatusTicketRepository, IDTicketRepository dticketRepository,
             IDTiempoAtencionRepository dTiempoAtencionRepository, IDFirmaRecibidoRepository dFirmaRecibidoRepository)
        {
            _logger = logger;
            _dprioridadRepository = dPrioridadRepository;
            _dPendientesRepository = dPendientesRepository;
            _dTiempoAtencionRepository = dTiempoAtencionRepository;
            _dfirmaRecibidoRepository = dFirmaRecibidoRepository;
            _ticketRepository = ticketRepository;
            _cat_EstatusTicketRepository = cat_EstatusTicketRepository;
            _dticketRepositoy = dticketRepository;
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
            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion(usuario);
            List<CountTicketViewModel> R_Ticket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_SeleccionarConteo(usuario);
            List<DTicketViewModel> dTickets = await _dticketRepositoy.GetDhasbord_Flujos_TotalTickets(usuario);
            List<DTicketViewModel> dbEtapasTotal = await _dticketRepositoy.GetDhasbord_Etapas_Operacion(usuario);
            List<DPrioridadViewModel> dbPrioridadTotal = await _dprioridadRepository.GetDhasbord_Prioridad_Operacion(usuario);
            DPendientesViewModel dbPendientes = await _dPendientesRepository.GetDhasbord_Pendientes_IdUsuario(usuario);
            List<DTiempoAtencionViewModel> dbTiempoAtencion = await _dTiempoAtencionRepository.GetDhasbord_Tiempos_Atencion(usuario);
            List<Cat_EstadoViewModel> dbTicketEstados = await _dticketRepositoy.GetDhasbord_Mapatikets(usuario);
            List<FirmaRecibidoViewModel> dbPregunta1 = await _dfirmaRecibidoRepository.GetDhasbord_Recibido_Encuesta_Pregunta1(usuario);
            List<FirmaRecibidoViewModel> dbPregunta2 = await _dfirmaRecibidoRepository.GetDhasbord_Recibido_Encuesta_Pregunta2(usuario);
            List<FirmaRecibidoViewModel> dbPregunta3 = await _dfirmaRecibidoRepository.GetDhasbord_Recibido_Encuesta_Pregunta3(usuario);

            ViewBag.MapaEstados = dbTicketEstados;
            ViewBag.Estatus = R_Ticket;
            ViewBag.Nombre = nombreUsuario;
            ViewBag.TicketFlujo = dTickets;
            ViewBag.Etapas = dbEtapasTotal;
            ViewBag.Tickets = db_tickets;
            ViewBag.Prioridad = dbPrioridadTotal;
            ViewBag.Pendiente = dbPendientes;
            ViewBag.TiempoAtencion = dbTiempoAtencion;
            ViewBag.Pregunta1 = dbPregunta1;
            ViewBag.Pregunta2 = dbPregunta2;
            ViewBag.Pregunta3 = dbPregunta3;

            return View();
        }
    }
}
