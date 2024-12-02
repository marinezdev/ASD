using ASD.Areas.Administracion.Models;
using ASD.Areas.Administracion.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Administracion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<dynamic> IniciarSesionAsync([FromBody] UsuarioViewModel usuario)
        {
            usuario.Password = Encriptar.EncriptarClave(usuario.Password);

            UsuariodbViewModel db_usuario = await _usuarioRepository.GetAutentificar(usuario);

            if (db_usuario.Usuario.Id > 0)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Sid, db_usuario.Usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, db_usuario.Persona.Nombre + " " +  db_usuario.Persona.ApellidoPaterno),
                    new Claim(ClaimTypes.Role, db_usuario.Usuario.Cat_Rol.Nombre.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties
                );
            }
            return (db_usuario);
        }
    }
}
