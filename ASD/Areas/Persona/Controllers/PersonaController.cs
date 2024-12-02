using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Persona.Models;
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.Persona;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using System.Collections.Generic;
using ASD.Areas.Administracion.Models.Consulta;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Empresa.Models;

namespace ASD.Areas.Persona.Controllers
{
    [Authorize]
    [Area("Persona")]

    public class PersonaController : Controller
    {
        private readonly IPersonaRepository _PersonaRepository;
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly ICuadrillaRepository _CuadrillaRepository;

        public PersonaController(IPersonaRepository personaRepository,IUsuarioRepository usuarioRepository, ICuadrillaRepository CuadrillaRepository)
        {
            _PersonaRepository = personaRepository;
            _UsuarioRepository = usuarioRepository;
            _CuadrillaRepository = CuadrillaRepository;
        }
        public virtual async Task<IActionResult> Listado()
        {
            List <CPersonaViewModel> db_Personas = await _PersonaRepository.GetPersonaListar();
            ViewBag.Personas = db_Personas;
            return View();
        }


        public virtual async Task<IActionResult> Editar()
        {
            if (!String.IsNullOrEmpty(Request.Query["Id"]))
            {
                int Id = Convert.ToInt32(Request.Query["Id"]);

                // Crear una instancia para almacenar el usuario
                CreateUserViewModel user = new CreateUserViewModel();
                user.Usuario = new UsuarioViewModel();
                user.Usuario.Id = Id;

                //PROCESOS PARA INTERFAZ
                List<Cat_RolViewModel> R_Roles = await _UsuarioRepository.ListarUsuarioRol();
                List<Cat_TipoEmailViewModel> R_email = await _PersonaRepository.GetCat_TipoEmailListar();
                List<Cat_TipoTelefonoViewModel> R_Tel = await _PersonaRepository.GetCat_TipoTelefonoListar();
                List<CuadrillaViewModel> R_Cuadrilla = await _CuadrillaRepository.GetCuadrilla_Seleccionar();

                ViewBag.Roles = R_Roles;
                ViewBag.Emails = R_email;
                ViewBag.Telefonos = R_Tel;
                ViewBag.Cuadrilla = R_Cuadrilla;
                //PROCESOS PARA CONSULTAR POR ID
                CreateUserViewModel dbUser = await _UsuarioRepository.GetUser_Id(user);
                ViewBag.User = dbUser;
                return View();
            }
            else
            {
                return RedirectToAction("listado", "Persona");
            }
        }

        public virtual async Task<IActionResult> Nueva()
        {
            List<Cat_RolViewModel> R_Roles = await _UsuarioRepository.ListarUsuarioRol();
            List<Cat_TipoEmailViewModel> R_email = await _PersonaRepository.GetCat_TipoEmailListar();
            List <Cat_TipoTelefonoViewModel > R_Tel = await _PersonaRepository.GetCat_TipoTelefonoListar();

            List<CuadrillaViewModel> R_Cuadrilla = await _CuadrillaRepository.GetCuadrilla_Seleccionar();

            ViewBag.Roles = R_Roles;
            ViewBag.Emails = R_email;
            ViewBag.Telefonos = R_Tel;
            ViewBag.Cuadrilla = R_Cuadrilla;

            return View();
        }



        [HttpPost]
        public async Task<JsonResult> DeletePerson([FromBody] PersonaViewModel persona)
        {
            PersonaViewModel R = await _PersonaRepository.DeletePerson(persona);

            return Json(R);
        }
    }
}
