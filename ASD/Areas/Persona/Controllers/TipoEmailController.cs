using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Persona.Models;
using ASD.Repository.Interface.Persona;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;

namespace ASD.Areas.Persona.Controllers
{
    [Authorize]
    [Area("Persona")]
    public class TipoEmailController : Controller
    {
        private readonly ICat_TipoEmailRepository _cat_TipoEmailRepository; 

        public TipoEmailController(ICat_TipoEmailRepository cat_TipoEmailRepository)
        {
            _cat_TipoEmailRepository = cat_TipoEmailRepository;
        }
        public virtual async Task<IActionResult> TipoEmail()
        {
            List<Cat_TipoEmailViewModel> r_cat_TipoEmail = await _cat_TipoEmailRepository.GetCat_TipoEmail_Seleccionar(); 
            ViewBag.CatTipoEmail = r_cat_TipoEmail;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetCat_TipoEmail_Seleccionar()
        {
            List<Cat_TipoEmailViewModel> r_cat_TipoEmail = await _cat_TipoEmailRepository.GetCat_TipoEmail_Seleccionar();
            return Json(r_cat_TipoEmail);
        }

        [HttpPost]
        public async Task<JsonResult> CreateCat_TipoEmail([FromBody] Cat_TipoEmailViewModel cat_TipoEmail)
        {
            Cat_TipoEmailViewModel Result_TipoEmail = await _cat_TipoEmailRepository.CreateCat_TipoEmail(cat_TipoEmail); 
            return Json(Result_TipoEmail);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_TipoEmail([FromBody] Cat_TipoEmailViewModel Cat_TipoEmail)
        {
            Cat_TipoEmailViewModel Result = await _cat_TipoEmailRepository.Delete_TipoEmail(Cat_TipoEmail);

            return Json(Result);
        }

        [HttpPost]
        public async Task<JsonResult> Update_TipoEmail([FromBody] Cat_TipoEmailViewModel Cat_TipoEmail)
        {
            Cat_TipoEmailViewModel R = await _cat_TipoEmailRepository.Update_TipoEmail(Cat_TipoEmail);

            return Json(R);
        }
    }
}
