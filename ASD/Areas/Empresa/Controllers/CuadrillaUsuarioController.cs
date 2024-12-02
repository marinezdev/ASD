using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Empresa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CuadrillaUsuarioController : Controller
    {
        private readonly ILogger<CuadrillaUsuarioController> _logger;
        private readonly ICuadrillaUsuarioRepository _cuadrillaUsuarioRepository;
        public CuadrillaUsuarioController(ILogger<CuadrillaUsuarioController> logger, ICuadrillaUsuarioRepository cuadrillaUsuarioRepository)
        {
            _logger = logger;
            _cuadrillaUsuarioRepository = cuadrillaUsuarioRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetCuadrillaUsuario_Seleccionar_IdCuadrilla([FromBody] CuadrillaViewModel cuadrilla)
        {
            List<CuadrillaUsuarioViewModel> db_Cuadrillas = await _cuadrillaUsuarioRepository.GetCuadrillaUsuario_Seleccionar_IdCuadrilla(cuadrilla);
            return Json(db_Cuadrillas);
        }
    }
}
