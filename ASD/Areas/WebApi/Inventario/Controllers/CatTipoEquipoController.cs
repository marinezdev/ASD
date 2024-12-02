using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatTipoEquipoController : ControllerBase
    {
        private readonly ILogger<CatTipoEquipoController> _logger;
        private readonly ICat_TipoEquipoRepository _cat_TipoEquipoRepository;

        public CatTipoEquipoController(ILogger<CatTipoEquipoController> logger, ICat_TipoEquipoRepository cat_TipoEquipoRepository)
        {
            _logger = logger;
            _cat_TipoEquipoRepository = cat_TipoEquipoRepository;
        }

        [HttpPost]
        [Route("GetCatTipoEquipo")]
        public async Task<dynamic> GetCat_TipoEquipo_SeleccionarAsync()
        {
            List<Cat_TipoEquipoViewModel> db_Cat_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            return (db_Cat_TipoEquipo);
        }

        [HttpPost]
        [Route("CreateCatTipoEquipo")]
        public async Task<dynamic> CreateCat_TipoEquipoAsync([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel db_Cat_TipoEquipo = await _cat_TipoEquipoRepository.CreateCat_TipoEquipo(cat_TipoEquipo);
            return (db_Cat_TipoEquipo);
        }


    }
}
