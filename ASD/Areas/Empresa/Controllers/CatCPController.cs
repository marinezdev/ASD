using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Empresa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CatCPController : Controller
    {
        private readonly ILogger<CatCPController> _logger;
        private readonly ICat_CPRepository _cat_CPRepository;

        public CatCPController(ILogger<CatCPController> logger, ICat_CPRepository cat_CPRepository)
        {
            _logger = logger;
            _cat_CPRepository = cat_CPRepository;
        }

        [HttpPost]
        public async Task<JsonResult> Cat_CP_Seleccionar_IdPoblacion([FromBody] Cat_PoblacionViewModel cat_Poblacion)
        {
            List<Cat_CPViewModel> db_Cat_CP = await _cat_CPRepository.GetCat_CP_Seleccionar_IdPoblacion(cat_Poblacion);
            return Json(db_Cat_CP);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCat_CP([FromBody] Cat_CPViewModel cat_CP)
        {
            Cat_CPViewModel db_Cat_CP = await _cat_CPRepository.CreateCat_CP(cat_CP);
            return Json(db_Cat_CP);
        }
    }
}
