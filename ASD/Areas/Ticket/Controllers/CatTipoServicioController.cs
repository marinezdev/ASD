using ASD.Areas.Administracion.Controllers;
using ASD.Areas.Administracion.Models.Consulta;
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class CatTipoServicioController : Controller
    {
        private readonly ILogger<CatTipoServicioController> _logger;
        private readonly ICat_TipoServicioRepository _cat_tipoServicioRepository;
        public CatTipoServicioController(ILogger<CatTipoServicioController> logger, ICat_TipoServicioRepository cat_TipoServicioRepository)
        {
            _logger = logger;
            _cat_tipoServicioRepository = cat_TipoServicioRepository;
        }

        [HttpPost]
        public async Task<JsonResult> Cat_TipoServicio_Seleccionar_IdFlujo([FromBody] FlujoViewModel flujo)
        {
            List<Cat_TipoServicioViewModel> db_Cat_TipoServicio = await _cat_tipoServicioRepository.GetCat_TipoServicio_Seleccionar_IdFlujo(flujo);
            return Json(db_Cat_TipoServicio);
        }

        [HttpPost]
        public async Task<JsonResult> Cat_TipoServicio_Seleccionar_Id([FromBody] Cat_TipoServicioViewModel cat_TipoServicio)
        {
            Cat_TipoServicioViewModel db_Cat_TipoServicio = await _cat_tipoServicioRepository.GetCat_TipoServicio_Seleccionar_Id(cat_TipoServicio);
            return Json(db_Cat_TipoServicio);
        }
    }
}
