using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Administracion;
using Microsoft.AspNetCore.Mvc;
using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Administracion.Controllers
{
    [Authorize]
    [Area("Administracion")]

    public class MenuController : Controller
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IMenuRepository _MenuRepository;


        public MenuController(IUsuarioRepository usuarioRepository, IMenuRepository menuRepository)
        {
            _UsuarioRepository = usuarioRepository;
            _MenuRepository = menuRepository;
        }

        public virtual async Task<IActionResult> Index()
        {
            List<Cat_RolViewModel> R_Roles = await _UsuarioRepository.ListarUsuarioRol();
            ViewBag.Roles = R_Roles;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetMenu_IdRol([FromBody] MenuViewModel MenuRol)
        {
            List<MenuViewModel> R = await _MenuRepository.GetMenu_IdRol(MenuRol);
            return Json(R);
        }


        [HttpPost]
        public async Task<JsonResult> Delete_Menu([FromBody] MenuViewModel MenuRol)
        {
            MenuViewModel R = await _MenuRepository.Delete_Menu(MenuRol);

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> GetCat_Menu_Listar_Faltante([FromBody] MenuViewModel MenuRol)
        {
            List<MenuViewModel> R = await _MenuRepository.GetCat_Menu_Listar_Faltante(MenuRol);
            return Json(R);
        }


        [HttpPost]
        public async Task<JsonResult> Insert_MenuPermiso([FromBody] MenuViewModel MenuRol)
        {
            MenuViewModel R = await _MenuRepository.Insert_MenuPermiso(MenuRol);

            return Json(R);
        }

    }
}