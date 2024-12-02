using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class Cat_EstatusFechaAtencionController : Controller
    {
        private readonly ICat_EstatusFechaAtencionRepository _cat_EstatusFechaAtencionRepository;

        public Cat_EstatusFechaAtencionController(ICat_EstatusFechaAtencionRepository cat_AsignacionEmpresaRepository)
        {
            _cat_EstatusFechaAtencionRepository = cat_AsignacionEmpresaRepository;

        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetCat_EstatusFechaAtencion()
        {
            List<Cat_EstatusFechaAtencionViewModel> R_Estatus = await _cat_EstatusFechaAtencionRepository.GetCat_EstatusFechaAtencion();

            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> InsertEstatusFechaAtencion([FromBody] Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion)
        {
            Cat_EstatusFechaAtencionViewModel R_Estatus = await _cat_EstatusFechaAtencionRepository.InsertEstatusFechaAtencion(cat_EstatusFechaAtencion);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEstatusFechaAtencion([FromBody] Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion)
        {
            Cat_EstatusFechaAtencionViewModel R_Estatus = await _cat_EstatusFechaAtencionRepository.DeleteEstatusFechaAtencion(cat_EstatusFechaAtencion);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateEstatusFechaAtencion([FromBody] Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion)
        {
            Cat_EstatusFechaAtencionViewModel R_Estatus = await _cat_EstatusFechaAtencionRepository.UpdateEstatusFechaAtencion(cat_EstatusFechaAtencion);
            return Json(R_Estatus);
        }
    }
    
}
