using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatCategoriaController : ControllerBase
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

        [HttpPost]
        [Route("GetCatCategoria")]
        public async Task<dynamic> GetCatCategoria_Seleccionar_IdTipoEquipoAsync([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_CategoriaViewModel> db_Cat_Categoria = await _cat_CategoriaRepository.GetCat_Categoria_Seleccionar_IdTipoEquipo(cat_TipoEquipo);
            return (db_Cat_Categoria);
        }

        [HttpPost]
        [Route("CreateCatCategoria")]
        public async Task<dynamic> CreateCat_Categoria_CrearAsync([FromBody] Cat_CategoriaViewModel cat_Categoria)
        {
            Cat_CategoriaViewModel db_Cat_Categoria = await _cat_CategoriaRepository.CreateCat_Categoria_Crear(cat_Categoria);
            return (db_Cat_Categoria);
        }

    }
}
