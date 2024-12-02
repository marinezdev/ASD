using ASD.Areas.Empresa.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Controllers;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CatPoblacionController : Controller
    {
        private readonly ILogger<CatPoblacionController> _logger;
        private readonly ICat_PoblacionRepository _cat_PoblacionRepository;

        public CatPoblacionController(ILogger<CatPoblacionController> logger, ICat_PoblacionRepository cat_PoblacionRepository)
        {
            _logger = logger;
            _cat_PoblacionRepository = cat_PoblacionRepository;
        }

        [HttpPost]
        public async Task<JsonResult> Cat_Poblacion_Seleccionar_IdEstado([FromBody] Cat_EstadoViewModel cat_Estado)
        {
            List<Cat_PoblacionViewModel> db_Cat_TipoServicio = await _cat_PoblacionRepository.GetCat_Poblacion_Seleccionar_IdEstado(cat_Estado);
            return Json(db_Cat_TipoServicio);
        }
        [HttpPost]
        public async Task<JsonResult> CreateCat_Poblacion([FromBody] Cat_PoblacionViewModel cat_Poblacion)
        {
            Cat_PoblacionViewModel db_CatPoblacion = await _cat_PoblacionRepository.CreateCat_Poblacion(cat_Poblacion);
            return Json(db_CatPoblacion);
        }
    }
}
