using ASD.Areas.Administracion.Controllers;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Persona;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Utilidades;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class EquipoUsuarioController : Controller
    {
        private readonly IEquipoUsuarioRepository _equipoUsuarioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly ICat_TipoEquipoRepository _cat_TipoEquipoRepository;
        private readonly ICat_EstatusEquipoRepository _cat_EstatusEquipoRepository;




        public EquipoUsuarioController(IEquipoUsuarioRepository equipoUsuarioRepository, IPersonaRepository personaRepository, ICat_TipoEquipoRepository cat_TipoEquipoRepository, ICat_EstatusEquipoRepository cat_EstatusEquipoRepository)
        {
            _equipoUsuarioRepository = equipoUsuarioRepository;
            _personaRepository = personaRepository;


            _cat_TipoEquipoRepository = cat_TipoEquipoRepository;
            _cat_EstatusEquipoRepository = cat_EstatusEquipoRepository;


        }

        [HttpGet()]
        public virtual async Task<IActionResult>AdministracionEquiposUsuario([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));


            EquipoUsuarioViewModel usuario = new EquipoUsuarioViewModel();
            usuario.Id = Convert.ToInt32(IdN);

            List<EquipoViewModel> db_equipo = await _equipoUsuarioRepository.GetEquipo_Seleccionar_Id(usuario);
            CPersonaViewModel db_Usuarioid = await _personaRepository.Persona_Seleccionar_IdUsuario(usuario);

            ViewBag.Equipo = db_equipo;
            ViewBag.Persona = db_Usuarioid;


            List<Cat_TipoEquipoViewModel> db_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            ViewBag.CatTipoEquipo = db_TipoEquipo;

            List<Cat_EstatusEquipoViewModel> db_EstatusEquipo = await _cat_EstatusEquipoRepository.GetCat_EstatusEquipo_Seleccionar();
            ViewBag.CatEstatusEquipo = db_EstatusEquipo;


            return View();
        }



        [HttpPost]
        public async Task<JsonResult> CreateEquipo([FromBody] EquipoViewModel equipo)
        {
 
            EquipoUsuarioViewModel EquipoUsuario = new EquipoUsuarioViewModel();
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            EquipoUsuario.Usuario = usuario;
            EquipoUsuario.Equipo = equipo;

            EquipoUsuarioViewModel db_EquipoUsuario = await _equipoUsuarioRepository.CreateEquipoUsuario(EquipoUsuario);

            return Json(db_EquipoUsuario);
        }

        [HttpPost]
        public async Task<JsonResult> GetEquipoUsuario_Seleccionar_IdUsuario()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }
            List<EquipoViewModel> db_Equipo = await _equipoUsuarioRepository.GetEquipoUsuario_Seleccionar_IdUsuario(usuario);
            return Json(db_Equipo);
        }



        public virtual async Task<IActionResult> AdministracionInventario()
        {
            List<EquipoUsuarioViewModel> db_Personas = await _equipoUsuarioRepository.GetPersonaListar();
            ViewBag.Personas = db_Personas;
            return View();
        }








        //[HttpPost]
        //public IActionResult URL_Cifrar([FromBody] EquipoUsuarioViewModel model)
        //{
        //    // Lógica para obtener los equipos asignados a la persona con Id = model.UrlVariable
        //    var equipos = _context.Equipos.Where(e => e.UsuarioId == model.UrlVariable).ToList();

        //    return Json(new { equipos });
        //}


        [HttpPost]
        public async Task<JsonResult> CreateEquipoPorUser([FromBody] EquipoViewModel equipo)
        {

            EquipoUsuarioViewModel EquipoUsuario = new EquipoUsuarioViewModel();
            //UsuarioViewModel usuario = new UsuarioViewModel();
            //string Id;
            //ClaimsPrincipal claimuser = HttpContext.User;

            //if (claimuser.Identity.IsAuthenticated)
            //{
            //    Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
            //    usuario.Id = Convert.ToInt32(Id);
            //}

            EquipoUsuario.Equipo = equipo;

            EquipoUsuarioViewModel db_EquipoUsuario = await _equipoUsuarioRepository.CreateEquipoUsuario(EquipoUsuario);

            return Json(db_EquipoUsuario);
        }



    }
}
