using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Areas.Persona.Models;
using ASD.Repository.Interface.Persona;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CuadrillaController : Controller
    {
        private readonly ICuadrillaRepository _CuadrillaRepository;
        private readonly IEmpresaRepository _EmpresaRepository;
        // GET: CuadrillaController

        public CuadrillaController(ICuadrillaRepository CuadrillaRepository, IEmpresaRepository empresaRepository)
        {
            _CuadrillaRepository = CuadrillaRepository;
            _EmpresaRepository = empresaRepository;
        }

        public virtual async Task<IActionResult>  ListCuadrilla()
        {
            List<EmpresaViewModel> db_Empresa = await _EmpresaRepository.GetEmpresa_Seleccionar();
            ViewBag.Empresa = db_Empresa;
            return View();
        }

        //[HttpPost]
        //public async Task<JsonResult> GetCuadrilla_Seleccionar()
        //{
        //    List<SucursalViewModel> db_Cuadrilla = await _CuadrillaRepository.GetCuadrilla_Seleccionar();
        //    return Json(db_Cuadrilla);
        //}

        [HttpPost]
        public async Task<JsonResult> GetCuadrillaIdEmpresa([FromBody] CuadrillaViewModel Cuadrilla)
        {
            List<CuadrillaViewModel> db_Cuadrilla = await _CuadrillaRepository.GetCuadrillaIdEmpresa(Cuadrilla);
            return Json(db_Cuadrilla);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCuadrilla([FromBody] CuadrillaViewModel Cuadrilla)
        {
            CuadrillaViewModel ResultCuadrilla = await _CuadrillaRepository.CreateCuadrilla(Cuadrilla);
            return Json(ResultCuadrilla);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_Cuadrilla([FromBody] CuadrillaViewModel Cuadrilla)
        {
            CuadrillaViewModel R = await _CuadrillaRepository.Delete_Cuadrilla(Cuadrilla);

            return Json(R);
        }



    }
}
