using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Empresa.Models;
using ASD.Areas.TicketAtencionMovil.Controllers;
using ASD.Repository.Interface.TicketAtencionMovil;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;


namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class CuadrillaZonaController : Controller
    {
        private readonly ICuadrillaZonaRepository _CuadrillaZonaRepository;
        private readonly ICat_EstadoRepository _cat_EstadoRepository;


        public CuadrillaZonaController(ICuadrillaZonaRepository CuadrillaZonaRepository, ICat_EstadoRepository cat_EstadoRepository)
        {
            _CuadrillaZonaRepository = CuadrillaZonaRepository;
            _cat_EstadoRepository = cat_EstadoRepository;

        }

        [HttpGet()]
        public virtual async Task<IActionResult> DetailCuadrilla([FromQuery(Name = "cont")] string Id)
        {
            CuadrillaZonaViewModel CuadrillaZona = new CuadrillaZonaViewModel();
            CuadrillaZona.Id = Convert.ToInt32(Id);

            ViewBag.Cuadrilla = CuadrillaZona;

            List<CuadrillaZonaViewModel> R = await _CuadrillaZonaRepository.Get_Cuadrilla(CuadrillaZona);
            ViewBag.CuadrillaZona = R;

            List<Cat_EstadoViewModel> db_CatEstados = await _cat_EstadoRepository.GetCat_Estado_Seleccionar();
            ViewBag.CatEstados = db_CatEstados;

            return View();
        }




        [HttpPost]
        public async Task<JsonResult> Delete_CuadrillaZona([FromBody] CuadrillaZonaViewModel CuadrillaZona)
        {
            CuadrillaZonaViewModel R = await _CuadrillaZonaRepository.Delete_CuadrillaZona(CuadrillaZona);

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Add_CuadrillaZona([FromBody] CuadrillaZonaViewModel CuadrillaZona)
        {
            CuadrillaZonaViewModel R = await _CuadrillaZonaRepository.Add_CuadrillaZona(CuadrillaZona);

            return Json(R);
        }
    }
}
