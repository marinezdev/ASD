using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;


namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class Cat_AsignacionEmpresaController : Controller
    {
        private readonly ICat_AsignacionEmpresaRepository _cat_AsignacionEmpresaRepository;

        public Cat_AsignacionEmpresaController(ICat_AsignacionEmpresaRepository cat_AsignacionEmpresaRepository)
        {
            _cat_AsignacionEmpresaRepository = cat_AsignacionEmpresaRepository;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetAsignacionEmpresa()
        {
            List<Cat_AsignacionEmpresaViewModel> R_Asignacion = await _cat_AsignacionEmpresaRepository.GetCat_AsignacionEmpresa_Seleccionar();

            return Json(R_Asignacion);
        }


        [HttpPost]
        public async Task<JsonResult> InsertAsignacionEmpresa([FromBody] Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa)
        {
            Cat_AsignacionEmpresaViewModel R_Asignacion = await _cat_AsignacionEmpresaRepository.InsertAsignacionEmpresa(cat_AsignacionEmpresa);
            return Json(R_Asignacion);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAsignacionEmpresa([FromBody] Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa)
        {
            Cat_AsignacionEmpresaViewModel R_Asignacion = await _cat_AsignacionEmpresaRepository.DeleteAsignacionEmpresa(cat_AsignacionEmpresa);
            return Json(R_Asignacion);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAsignacionEmpresa([FromBody] Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa)
        {
            Cat_AsignacionEmpresaViewModel R_Asignacion = await _cat_AsignacionEmpresaRepository.UpdateAsignacionEmpresa(cat_AsignacionEmpresa);
            return Json(R_Asignacion);
        }

    }
}
