using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class CatTipoEquipoImagenController : Controller
    {
        private readonly ILogger<CatTipoEquipoImagenController> _logger;
        private readonly ICat_TipoEquipoImagenRepository _cat_TipoEquipoImagenRepository;

        public CatTipoEquipoImagenController(ILogger<CatTipoEquipoImagenController> logger, ICat_TipoEquipoImagenRepository cat_TipoEquipoImagenRepository)
        {
            _logger = logger;
            _cat_TipoEquipoImagenRepository = cat_TipoEquipoImagenRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetCat_TipoEquipoImagen_Seleccionar_IdTipoEquipo([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();
            List<Cat_TipoEquipoImagenViewModel> db_Cat_TipoEquipoImagen = await _cat_TipoEquipoImagenRepository.GetCat_TipoEquipoImagen(cat_TipoEquipo);

            if (db_Cat_TipoEquipoImagen != null)
            {
                foreach (var TipoEquipoImagen in db_Cat_TipoEquipoImagen)
                {
                    var sesionEquipoImagen = new Cat_EquipoImagenViewModel();

                    sesionEquipoImagen.Id = TipoEquipoImagen.Id;
                    sesionEquipoImagen.Nombre = TipoEquipoImagen.Nombre;
                    sesionEquipoImagen.Ordenamiento = TipoEquipoImagen.Ordenamiento;
                    sesionEquipoImagen.Observaciones = TipoEquipoImagen.Observaciones;

                    sesion_EquipoImagen.Add(sesionEquipoImagen);
                }
            }
            

            //PORQUE ESTO ESTABA COMENTADO??
            //HttpContext.Session.Set<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen", sesion_EquipoImagen);

            return Json(sesion_EquipoImagen);
        }

    }
}
