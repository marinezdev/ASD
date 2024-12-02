using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class CatModeloController : Controller
    {
        private readonly ILogger<CatModeloController> _logger;
        private readonly ICat_ModeloRepository _cat_ModeloRepository;
        private readonly ICat_TipoEquipoRepository _Cat_TipoEquipoRepository;


        public CatModeloController(ILogger<CatModeloController> logger, ICat_ModeloRepository cat_ModeloRepository, ICat_TipoEquipoRepository cat_TipoEquipoRepository)
        {
            _logger = logger;
            _cat_ModeloRepository = cat_ModeloRepository;
            _Cat_TipoEquipoRepository = cat_TipoEquipoRepository;
        }

        public virtual async Task<IActionResult> Modelo()
        {
            List<Cat_TipoEquipoViewModel> db_Cat_TipoEquipo = await _Cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            ViewBag.CatTipoEquipo = db_Cat_TipoEquipo;

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetCat_Modelo_Seleccionar_IdCategoria([FromBody] Cat_CategoriaViewModel cat_Categoria)
        {
            List<Cat_ModeloViewModel> db_Cat_Modelo = await _cat_ModeloRepository.GetCat_Modelo_Seleccionar_IdCategoria(cat_Categoria);
            return Json(db_Cat_Modelo);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCat_Modelo([FromBody] Cat_ModeloViewModel cat_Modelo)
        {
            Cat_ModeloViewModel db_Cat_Modelo = await _cat_ModeloRepository.CreateCat_Modelo_Crear(cat_Modelo);
            return Json(db_Cat_Modelo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_Modelo([FromBody] Cat_ModeloViewModel cat_Modelo)
        {
            Cat_ModeloViewModel R = await _cat_ModeloRepository.Delete_Modelo(cat_Modelo);

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Update_Modelo([FromBody] Cat_ModeloViewModel cat_Modelo)
        {
            Cat_ModeloViewModel R = await _cat_ModeloRepository.Update_Modelo(cat_Modelo);

            return Json(R);
        }

    }
}
