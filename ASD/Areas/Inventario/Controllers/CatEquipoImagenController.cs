using ASD.Areas.Administracion.Controllers;
using ASD.Areas.Empresa.Models;
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
    public class CatEquipoImagenController : Controller
    {
        private readonly ILogger<CatEquipoImagenController> _logger;
        public CatEquipoImagenController(ILogger<CatEquipoImagenController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public async Task<JsonResult> AddListEquipoImagen([FromBody] Cat_EquipoImagenViewModel cat_EquipoImagen)
        {
            // Dejar este bloque de codigo por que no refresca la variable de sesion
            List<Cat_EquipoRutinaViewModel> sesion_EquipoRutina = new List<Cat_EquipoRutinaViewModel>();
            if (HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina") != null)
            {
                sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");
                HttpContext.Session.Set<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina", sesion_EquipoRutina);
            }
            //-------------------------------------------------------------------------------------------
            
            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();
            if (HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen") != null)
            {
                sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");
                cat_EquipoImagen.Id = sesion_EquipoImagen.Last().Id + 1;
            }
            else
            {
                cat_EquipoImagen.Id = 1;
            }

            sesion_EquipoImagen.Add(cat_EquipoImagen);
            HttpContext.Session.Set<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen", sesion_EquipoImagen);
      


            return Json(sesion_EquipoImagen);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteListEquipoImagen([FromBody] Cat_EquipoImagenViewModel cat_EquipoImagen)
        {
            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();

            if (HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen") != null)
            {
                sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");
                int index = sesion_EquipoImagen.FindIndex(x => x.Id == cat_EquipoImagen.Id);
                sesion_EquipoImagen.RemoveAt(index);
            }

            if (sesion_EquipoImagen.Count == 0)
            {
                HttpContext.Session.Remove("ListEquipoImagen");
            }
            else
            {
                HttpContext.Session.Set<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen", sesion_EquipoImagen);
            }

            return Json(sesion_EquipoImagen);
        }
    }
}
