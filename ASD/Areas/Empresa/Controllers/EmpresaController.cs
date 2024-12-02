using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Empresa;


namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {


            return View();
        }

        private readonly IEmpresaRepository _EmpresaRepository;

        public EmpresaController(IEmpresaRepository EmpresaRepository)
        {
            _EmpresaRepository = EmpresaRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetEmpresa_Seleccionar()
        {
            List<EmpresaViewModel> db_Empresa = await _EmpresaRepository.GetEmpresa_Seleccionar();
            return Json(db_Empresa);
        }
    }
}
