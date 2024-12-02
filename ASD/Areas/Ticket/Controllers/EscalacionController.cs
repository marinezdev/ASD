using ASD.Areas.Administracion.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Persona;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class EscalacionController : Controller
    {
        private readonly ILogger<EscalacionController> _logger;
        private readonly IFlujoRepository _flujoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEscalacionRepository _ecscalacionRepository;
        private readonly IPersonaRepository _PersonaRepository;

        public EscalacionController(ILogger<EscalacionController> logger, IFlujoRepository flujoRepository, ITicketRepository ticketRepository, IEscalacionRepository ecscalacionRepository, IPersonaRepository personaRepository)
        {
            _logger = logger;
            _flujoRepository = flujoRepository;
            _ticketRepository = ticketRepository;
            _ecscalacionRepository = ecscalacionRepository;
            _PersonaRepository = personaRepository;
        }

        // GET: EscalacionController
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

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion(usuario);
            List<FlujoViewModel> db_Flujos = await _flujoRepository.GetFlujo_Seleccionar();
            List<CPersonaViewModel> db_Personas = await _PersonaRepository.GetPersonaListar();

            ViewBag.Usuario = db_Personas;
            ViewBag.Flujos = db_Flujos;
            ViewBag.Tickets = db_tickets;

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CreateEscalacion([FromBody] EscalacionViewModel escalacion)
        {
            EscalacionViewModel db_Escalacion = await _ecscalacionRepository.CreateEscalacion_Insertar(escalacion);

            return Json(db_Escalacion);
        }

        [HttpPost]
        public async Task<JsonResult> GetEscalacion_Seleccionar_IdFlujo([FromBody] EscalacionViewModel escalacion)
        {
            List<CEscalacionViewModel> db_Escalacion = await _ecscalacionRepository.GetEscalacion_Seleccionar_IdFlujo(escalacion);
            return Json(db_Escalacion);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEscalacion([FromBody] EscalacionViewModel escalacion)
        {
            EscalacionViewModel db_Escalacion = await _ecscalacionRepository.DeleteEscalacion_Eliminar(escalacion);
            return Json(db_Escalacion);
        }
    }
}
