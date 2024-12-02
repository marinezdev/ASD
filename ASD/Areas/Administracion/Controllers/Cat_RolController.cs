using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Administracion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Administracion.Controllers
{
    [Authorize]
    [Area("Administracion")]

    public class Cat_RolController : Controller
    {
        private readonly ICat_RolRepository _cat_RolRepository;

        public Cat_RolController(ICat_RolRepository Cat_RolRepository)
        {
            _cat_RolRepository = Cat_RolRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetCat_Cat_Rol_Listar_IdUsuario([FromBody] Cat_RolViewModel Cat_Rol)
        {
            List<Cat_RolViewModel> R = await _cat_RolRepository.GetCat_Cat_Rol_Listar_IdUsuario(Cat_Rol);
            return Json(R);
        }
    }
}