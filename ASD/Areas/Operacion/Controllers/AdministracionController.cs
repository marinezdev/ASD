using ASD.Areas.Persona.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class AdministracionController : Controller
    {
        private readonly IPersonaRepository _PersonaRepository;

        public AdministracionController(IPersonaRepository personaRepository)
        {
            _PersonaRepository = personaRepository;

        }

        public virtual async Task<IActionResult> Index()
        {
            List<CPersonaViewModel> db_Personas = await _PersonaRepository.GetPersonaListar();
            ViewBag.Usuario = db_Personas;

            return View();
        }
    }
}
