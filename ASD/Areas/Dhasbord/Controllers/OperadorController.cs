using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Repository.Interface.Dhasbord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.Dhasbord.Controllers
{
    [Authorize]
    [Area("Dhasbord")]
    public class OperadorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDPrioridadRepository _dprioridadRepository;
        private readonly IDPendientesRepository _dPendientesRepository;

        public OperadorController(ILogger<HomeController> logger, IDPrioridadRepository dPrioridadRepository, IDPendientesRepository dPendientesRepository)
        {
            _logger = logger;
            _dprioridadRepository = dPrioridadRepository;
            _dPendientesRepository = dPendientesRepository;
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
            List<DPrioridadViewModel> dbPrioridadTotal = await _dprioridadRepository.GetDhasbord_Prioridad_Operacion(usuario);
            DPendientesViewModel dbPendientes = await _dPendientesRepository.GetDhasbord_Pendientes_IdUsuario(usuario);

            ViewBag.Nombre = nombreUsuario;
            ViewBag.Prioridad = dbPrioridadTotal;
            ViewBag.Pendiente = dbPendientes;

            return View();
        }
    }
}
