using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASD.Areas.Administracion.Models.Consulta;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Utilidades;


namespace ASD.Areas.Administracion.Controllers
{
    [Authorize]
    [Area("Administracion")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody] CreateUserViewModel usuario)
        {
            usuario.Usuario.Password = Encriptar.EncriptarClave(usuario.Usuario.Password);
            CreateUserViewModel db_User = await _usuarioRepository.SaveUsuario(usuario);
            return Json(db_User);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUser([FromBody]  CreateUserViewModel usuario)
        {
            CreateUserViewModel db_User = await _usuarioRepository.UpdateUser(usuario);
            return Json(db_User);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUserPass([FromBody] CreateUserViewModel usuario)
        {
            usuario.Usuario.Password = Encriptar.EncriptarClave(usuario.Usuario.Password);

            CreateUserViewModel db_User = await _usuarioRepository.UpdateUserPass(usuario);
            return Json(db_User);
        }

    }
}