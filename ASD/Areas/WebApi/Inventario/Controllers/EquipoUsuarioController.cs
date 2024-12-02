using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Inventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoUsuarioController : ControllerBase
    {
        private readonly ILogger<EquipoUsuarioController> _logger;
        private readonly IEquipoUsuarioRepository _equipoUsuarioRepository;

        public EquipoUsuarioController(ILogger<EquipoUsuarioController> logger, IEquipoUsuarioRepository equipoUsuarioRepository)
        {
            _logger = logger;
            _equipoUsuarioRepository = equipoUsuarioRepository;
        }

        [HttpPost]
        [Route("GetEquipoUsuario")]
        public async Task<dynamic> GetEquipoUsuario_Seleccionar_IdUsuarioAsync([FromBody] UsuarioViewModel usuario)
        {
            List<EquipoViewModel> db_Equipo = await _equipoUsuarioRepository.GetEquipoUsuario_Seleccionar_IdUsuario(usuario);
            return (db_Equipo);
        }


        [HttpPost]
        [Route("CreateEquipoUsuario")]
        public async Task<dynamic> CreateEquipoAsync([FromBody] EquipoUsuarioViewModel EquipoUsuario)
        {
            EquipoUsuarioViewModel db_EquipoUsuario = await _equipoUsuarioRepository.CreateEquipoUsuario(EquipoUsuario);
            return (db_EquipoUsuario);
        }
    }
}
