using ASD.Areas.Operacion.Models;
using ASD.Repository.Interface.Operacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class FlujoController : Controller
    {
        private readonly IFlujoRepository _flujoRepository;

        public FlujoController(IFlujoRepository flujoRepository)
        {
            _flujoRepository = flujoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetCat_Flujo_Listar_IdUsuario([FromBody] FlujoViewModel Flujo)
        {
            List<FlujoViewModel> R = await _flujoRepository.GetCat_Flujo_Listar_IdUsuario(Flujo);
            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> GetCat_Flujo_Listar_Faltante([FromBody] UsuarioFlujoViewModel Flujo)
        {
            List<FlujoViewModel> R = await _flujoRepository.GetCat_Flujo_Listar_Faltante(Flujo);
            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Add_UsuarioFlujo([FromBody] UsuarioFlujoViewModel UsuarioFlujo)
        {
            UsuarioFlujoViewModel R = await _flujoRepository.Add_UsuarioFlujo(UsuarioFlujo);

            return Json(R);
        }
    }
}
