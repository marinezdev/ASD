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
    public class CatTipoEquipoRutinaController : Controller
    {
        private readonly ILogger<CatTipoEquipoRutinaController> _logger;
        private readonly ICat_TipoEquipoRutinaRepository _cat_TipoEquipoRutinaRepository;
        private readonly ICat_TipoEquipoImagenRepository _cat_TipoEquipoImagenRepository;

        public CatTipoEquipoRutinaController(ILogger<CatTipoEquipoRutinaController> logger, ICat_TipoEquipoRutinaRepository cat_TipoEquipoRutinaRepository,ICat_TipoEquipoImagenRepository cat_TipoEquipoImagenRepository)
        {
            _logger = logger;
            _cat_TipoEquipoRutinaRepository = cat_TipoEquipoRutinaRepository;
            _cat_TipoEquipoImagenRepository = cat_TipoEquipoImagenRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetCat_TipoEquipoRutina_Seleccionar_IdTipoEquipo([FromBody] Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_EquipoRutinaViewModel> sesion_EquipoRutina = new List<Cat_EquipoRutinaViewModel>();
            List<Cat_TipoEquipoRutinaViewModel> db_Cat_TipoEquipoRutina = await _cat_TipoEquipoRutinaRepository.GetCat_TipoEquipoRutina(cat_TipoEquipo);

            if (db_Cat_TipoEquipoRutina != null)
            {
                foreach (var tipoRutina in db_Cat_TipoEquipoRutina)
                {
                    var sesionRutina = new Cat_EquipoRutinaViewModel();

                    sesionRutina.Id = tipoRutina.Id;
                    sesionRutina.Nombre = tipoRutina.Nombre;
                    sesionRutina.Ordenamiento = tipoRutina.Ordenamiento;
                    sesionRutina.Observaciones = tipoRutina.Observaciones;

                    sesion_EquipoRutina.Add(sesionRutina);
                }
            }
            
            HttpContext.Session.Set<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina", sesion_EquipoRutina);

            

            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();
            List<Cat_TipoEquipoImagenViewModel> db_Cat_TipoEquipoImagen = await _cat_TipoEquipoImagenRepository.GetCat_TipoEquipoImagen(cat_TipoEquipo);

            foreach (var TipoEquipoImagen in db_Cat_TipoEquipoImagen)
            {
                var sesionEquipoImagen = new Cat_EquipoImagenViewModel();

                sesionEquipoImagen.Id = TipoEquipoImagen.Id;
                sesionEquipoImagen.Nombre = TipoEquipoImagen.Nombre;
                sesionEquipoImagen.Ordenamiento = TipoEquipoImagen.Ordenamiento;
                sesionEquipoImagen.Observaciones = TipoEquipoImagen.Observaciones;

                sesion_EquipoImagen.Add(sesionEquipoImagen);
            }

            HttpContext.Session.Set<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen", sesion_EquipoImagen);

            return Json(sesion_EquipoRutina);

        }
    }
}
