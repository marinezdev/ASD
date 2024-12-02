using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Controllers;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CatEstadoController : Controller
    {

        private readonly ILogger<CatEstadoController> _logger;
        private readonly ICat_EstadoRepository _cat_estadoRepository;
        public CatEstadoController(ILogger<CatEstadoController> logger, ICat_EstadoRepository cat_EstadoRepository)
        {
            _logger = logger;
            _cat_estadoRepository = cat_EstadoRepository;
        }

        [HttpPost]
        public async Task<JsonResult> Cat_Estado_Seleccionar()
        {
            List<Cat_EstadoViewModel> db_CatEstado = await _cat_estadoRepository.GetCat_Estado_Seleccionar();
            return Json(db_CatEstado);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCat_Estado([FromBody] Cat_EstadoViewModel cat_Estado)
        {
            Cat_EstadoViewModel db_CatEstado = await _cat_estadoRepository.CreateCat_Estado(cat_Estado);
            return Json(db_CatEstado);
        }

    }
}
