using ASD.Areas.Administracion.Controllers;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Utilidades;
using Irony.Parsing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class CatEquipoRutinaController : Controller
    {
        private readonly ILogger<CatEquipoRutinaController> _logger;
        public CatEquipoRutinaController(ILogger<CatEquipoRutinaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<JsonResult> AddListEquipoRutina([FromBody] Cat_EquipoRutinaViewModel cat_EquipoRutina)
        {
            // Dejar este bloque de codigo por que no refresca la variable de sesion
            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();
            if (HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen") != null)
            {
                sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");
                HttpContext.Session.Set<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen", sesion_EquipoImagen);
            }
            //-------------------------------------------------------------------------------------------
            List<Cat_EquipoRutinaViewModel> sesion_EquipoRutina = new List<Cat_EquipoRutinaViewModel>();
            if (HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina") != null)
            {
                sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");
                cat_EquipoRutina.Id = sesion_EquipoRutina.Last().Id + 1;
            }
            else
            {
                cat_EquipoRutina.Id = 1;
            }

            sesion_EquipoRutina.Add(cat_EquipoRutina);
            HttpContext.Session.Set<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina", sesion_EquipoRutina);

            return Json(sesion_EquipoRutina);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteListEquipoRutina([FromBody] Cat_EquipoRutinaViewModel cat_EquipoRutina)
        {
            List<Cat_EquipoRutinaViewModel> sesion_EquipoRutina = new List<Cat_EquipoRutinaViewModel>();

            if (HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina") != null)
            {
                sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");
                int index = sesion_EquipoRutina.FindIndex(x => x.Id == cat_EquipoRutina.Id);
                sesion_EquipoRutina.RemoveAt(index);
            }

            if (sesion_EquipoRutina.Count == 0)
            {
                HttpContext.Session.Remove("ListEquipoRutina");
            }
            else
            {
                HttpContext.Session.Set<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina", sesion_EquipoRutina);
            }

            return Json(sesion_EquipoRutina);
        }

    }
}
