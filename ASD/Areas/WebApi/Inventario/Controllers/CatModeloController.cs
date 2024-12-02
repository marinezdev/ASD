using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatModeloController : ControllerBase
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

        [HttpPost]
        [Route("GetCatModelo")]
        public async Task<dynamic> GetCatModelo_Seleccionar_IdCategoriaAsync([FromBody] Cat_CategoriaViewModel cat_Categoria)
        {
            List<Cat_ModeloViewModel> db_Cat_Modelo = await _cat_ModeloRepository.GetCat_Modelo_Seleccionar_IdCategoria(cat_Categoria);
            return (db_Cat_Modelo);
        }

        [HttpPost]
        [Route("CreateCatModelo")]
        public async Task<dynamic> CreateCat_ModeloAsync([FromBody] Cat_ModeloViewModel cat_Modelo)
        {
            Cat_ModeloViewModel db_Cat_Modelo = await _cat_ModeloRepository.CreateCat_Modelo_Crear(cat_Modelo);
            return (db_Cat_Modelo);
        }
    }
}
