using ASD.Areas.Operacion.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Persona;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class Cat_EstatusTicketEtapaController : Controller
    {
        private readonly ICat_EstatusTicketEtapaRepository _cat_EstatusTicketEtapaRepository;

        public Cat_EstatusTicketEtapaController(ICat_EstatusTicketEtapaRepository cat_EstatusTicketEtapaRepository)
        {
            _cat_EstatusTicketEtapaRepository = cat_EstatusTicketEtapaRepository;

        }

        public virtual async Task<IActionResult> TipoEstatusEtapa()
        {
            List<Cat_EstatusTicketEtapaViewModel> R_Estatus = await _cat_EstatusTicketEtapaRepository.GetCat_TipoEstatusEtapa();
            ViewBag.EstatusEtapa = R_Estatus;
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetCat_TipoEstatusEtapa()
        {
            List<Cat_EstatusTicketEtapaViewModel> R_Estatus = await _cat_EstatusTicketEtapaRepository.GetCat_TipoEstatusEtapa();

            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> InsertTipoEstatusEtapa([FromBody] Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa)
        {
            Cat_EstatusTicketEtapaViewModel R_Estatus = await _cat_EstatusTicketEtapaRepository.InsertTipoEstatusEtapa(Cat_EstatusTicketEtapa);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTipoEstatusEtapa([FromBody] Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa)
        {
            Cat_EstatusTicketEtapaViewModel R_EstatusE = await _cat_EstatusTicketEtapaRepository.DeleteTipoEstatusEtapa(Cat_EstatusTicketEtapa);
            return Json(R_EstatusE);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTipoEstatusEtapa([FromBody] Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa)
        {
            Cat_EstatusTicketEtapaViewModel R_Estatus = await _cat_EstatusTicketEtapaRepository.UpdateTipoEstatusEtapa(Cat_EstatusTicketEtapa);
            return Json(R_Estatus);
        }
    }
}
