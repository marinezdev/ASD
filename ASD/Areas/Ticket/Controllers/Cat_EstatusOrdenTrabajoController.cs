using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class Cat_EstatusOrdenTrabajoController : Controller
    {
        private readonly ICat_EstatusOrdenTrabajoRepository _ICat_EstatusOrdenTrabajoRepository;

        public Cat_EstatusOrdenTrabajoController(ICat_EstatusOrdenTrabajoRepository cat_EstatusOrdenTrabajoRepository)
        {
            _ICat_EstatusOrdenTrabajoRepository = cat_EstatusOrdenTrabajoRepository;

        }
        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public async Task<JsonResult> GetCat_EstatusOrdenTrabajo_Seleccionar()
        {
            List<Cat_EstatusOrdenTrabajoViewModel> R_Orden = await _ICat_EstatusOrdenTrabajoRepository.GetCat_EstatusOrdenTrabajo_Seleccionar();

            return Json(R_Orden);
        }

        [HttpPost]
        public async Task<JsonResult> InsertEstatusOrdenTrabajo([FromBody] Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo)
        {
            Cat_EstatusOrdenTrabajoViewModel R_Orden = await _ICat_EstatusOrdenTrabajoRepository.InsertEstatusOrdenTrabajo(cat_EstatusOrdenTrabajo);
            return Json(R_Orden);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEstatusOrdenTrabajo([FromBody] Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo)
        {
            Cat_EstatusOrdenTrabajoViewModel R_Orden = await _ICat_EstatusOrdenTrabajoRepository.DeleteEstatusOrdenTrabajo(cat_EstatusOrdenTrabajo);
            return Json(R_Orden);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateEstatusOrdenTrabajo([FromBody] Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo)
        {
            Cat_EstatusOrdenTrabajoViewModel R_Orden = await _ICat_EstatusOrdenTrabajoRepository.UpdateEstatusOrdenTrabajo(cat_EstatusOrdenTrabajo);
            return Json(R_Orden);
        }
    }
}
