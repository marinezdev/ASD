using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class CatCategoriaController : Controller
    {
        private readonly ILogger<CatCategoriaController> _logger;
        private readonly ICat_CategoriaRepository _cat_CategoriaRepository;
        private readonly ICat_TipoEquipoRepository _cat_TipoEquipoRepository;


        public CatCategoriaController(ILogger<CatCategoriaController> logger, ICat_CategoriaRepository cat_CategoriaRepository, ICat_TipoEquipoRepository cat_TipoEquipoRepository)
        {
            _logger = logger;
            _cat_CategoriaRepository = cat_CategoriaRepository;
            _cat_TipoEquipoRepository = cat_TipoEquipoRepository;

        }


        public virtual async Task<IActionResult> Categoria()
        {
            List<Cat_TipoEquipoViewModel> db_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            ViewBag.CatTipoEquipo = db_TipoEquipo;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCat_Categoria_Seleccionar_IdTipoEquipo([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_CategoriaViewModel> db_Cat_Categoria = await _cat_CategoriaRepository.GetCat_Categoria_Seleccionar_IdTipoEquipo(cat_TipoEquipo);
            return Json(db_Cat_Categoria);
        }





        [HttpPost]
        public async Task<JsonResult> CreateCat_TipoEquipo([FromBody] Cat_CategoriaViewModel cat_Categoria)
        {
            Cat_CategoriaViewModel db_Cat_Categoria = await _cat_CategoriaRepository.CreateCat_Categoria_Crear(cat_Categoria);
            return Json(db_Cat_Categoria);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_Categoria([FromBody] Cat_CategoriaViewModel cat_Categoria)
        {
            Cat_CategoriaViewModel R = await _cat_CategoriaRepository.Delete_Categoria(cat_Categoria);

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Update_Categoria([FromBody] Cat_CategoriaViewModel cat_Categoria)
        {
            Cat_CategoriaViewModel R = await _cat_CategoriaRepository.Update_Categoria(cat_Categoria);

            return Json(R);
        }
    }
}
