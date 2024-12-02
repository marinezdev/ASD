using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Empresa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CatColoniaController : Controller
    {
        private readonly ILogger<CatColoniaController> _logger;
        private readonly ICat_ColoniaRepository _cat_ColoniaRepository;

        public CatColoniaController(ILogger<CatColoniaController> logger, ICat_ColoniaRepository cat_ColoniaRepository)
        {
            _logger = logger;
            _cat_ColoniaRepository = cat_ColoniaRepository;
        }

        [HttpPost]
        public async Task<JsonResult> Cat_Colonia_Seleccionar_IdCP([FromBody] Cat_CPViewModel cat_CP)
        {
            List<Cat_ColoniaViewModel> db_Cat_Colonia = await _cat_ColoniaRepository.GetCat_Colonia_Seleccionar_IdCP(cat_CP);
            return Json(db_Cat_Colonia);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCat_Colonia([FromBody] Cat_ColoniaViewModel cat_Colonia)
        {
            Cat_ColoniaViewModel db_Cat_Colonia = await _cat_ColoniaRepository.CreateCat_Colonia(cat_Colonia);
            return Json(db_Cat_Colonia);
        }

    }
}
