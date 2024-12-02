using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class CatTipoEquipoController : Controller
    {
        private readonly ILogger<CatTipoEquipoController> _logger;
        private readonly ICat_TipoEquipoRepository _cat_TipoEquipoRepository;

        public CatTipoEquipoController(ILogger<CatTipoEquipoController> logger, ICat_TipoEquipoRepository cat_TipoEquipoRepository)
        {
            _logger = logger;
            _cat_TipoEquipoRepository = cat_TipoEquipoRepository;
        }

        public virtual async Task<IActionResult> TipoEquipo()
        {
            List<Cat_TipoEquipoViewModel> db_Cat_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            ViewBag.CatTipoEquipo = db_Cat_TipoEquipo;

            return View();
        }



        [HttpPost]
        public async Task<JsonResult> GetCat_TipoEquipo_Seleccionar()
        {
            List<Cat_TipoEquipoViewModel> db_Cat_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            return Json(db_Cat_TipoEquipo);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCat_TipoEquipo([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel db_Cat_TipoEquipo = await _cat_TipoEquipoRepository.CreateCat_TipoEquipo(cat_TipoEquipo);
            HttpContext.Session.Remove("ListEquipoRutina");
            HttpContext.Session.Remove("ListEquipoImagen");
            return Json(db_Cat_TipoEquipo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_TipoEquipo([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel R = await _cat_TipoEquipoRepository.Delete_TipoEquipo(cat_TipoEquipo);

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Update_TipoEquipo([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel R = await _cat_TipoEquipoRepository.Update_TipoEquipo(cat_TipoEquipo);

            return Json(R);
        }
    }
}
