using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class Cat_PrioridadController : Controller
    {
        private readonly ICat_PrioridadRepository _cat_PrioridadRepository;

        public Cat_PrioridadController(ICat_PrioridadRepository cat_PrioridadRepository)
        {
            _cat_PrioridadRepository = cat_PrioridadRepository;

        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<JsonResult> GetCat_Prioridad_Seleccionar()
        {
            List<Cat_PrioridadViewModel> R_cat_Prioridad = await _cat_PrioridadRepository.GetCat_Prioridad_Seleccionar();

            return Json(R_cat_Prioridad);
        }

        [HttpPost]
        public async Task<JsonResult> InsertPrioridad([FromBody] Cat_PrioridadViewModel cat_Prioridad)
        {
            Cat_PrioridadViewModel R_cat_Prioridad = await _cat_PrioridadRepository.InsertPrioridad(cat_Prioridad);
            return Json(R_cat_Prioridad);
        }

        [HttpPost]
        public async Task<JsonResult> DeletePrioridad([FromBody] Cat_PrioridadViewModel cat_Prioridad)
        {
            Cat_PrioridadViewModel R_cat_Prioridad = await _cat_PrioridadRepository.DeletePrioridad(cat_Prioridad);
            return Json(R_cat_Prioridad);
        }

        [HttpPost]
        public async Task<JsonResult> UpdatePrioridad([FromBody] Cat_PrioridadViewModel cat_Prioridad)
        {
            Cat_PrioridadViewModel R_cat_Prioridad = await _cat_PrioridadRepository.UpdatePrioridad(cat_Prioridad);
            return Json(R_cat_Prioridad);
        }
    }
}
