using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Repository.Interface.Persona;
using System.Collections.Generic;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Ticket;

namespace ASD.Areas.Empresa.Controllers
{
    [Authorize]
    [Area("Empresa")]
    public class SucursalController : Controller
    {
        private readonly ILogger<SucursalController> _logger;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IEmpresaRepository _EmpresaRepository;
        private readonly ICat_EstadoRepository _cat_EstadoRepository;
        private readonly ICat_TipoSucursalRepository _cat_TipoSucursalRepository;



        public SucursalController(ILogger<SucursalController> logger, ISucursalRepository sucursalRepository, IEmpresaRepository EmpresaRepository,
             ICat_EstadoRepository cat_EstadoRepository,ICat_TipoSucursalRepository cat_TipoSucursalRepository)
        {
            _logger = logger;
            _sucursalRepository = sucursalRepository;
            _EmpresaRepository = EmpresaRepository;
            _cat_EstadoRepository = cat_EstadoRepository;
            _cat_TipoSucursalRepository = cat_TipoSucursalRepository;
        }

        public virtual async Task<IActionResult> Index()
        
        {
            List<Cat_EstadoViewModel> db_CatEstados = await _cat_EstadoRepository.GetCat_Estado_Seleccionar();
            List<Cat_TipoSucursalViewModel> db_TipoSucursal = await _cat_TipoSucursalRepository.GetCat_TipoSucursal_Seleccionar();

            List<EmpresaViewModel> db_Empresa = await _EmpresaRepository.GetEmpresa_Seleccionar();
            ViewBag.Empresa = db_Empresa;
            ViewBag.CatEstados = db_CatEstados;
            ViewBag.CatTipoSucursal = db_TipoSucursal;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetSucursal_Seleccionar()
        {
            List<SucursalViewModel> db_Sucursal = await _sucursalRepository.GetSucursal_Seleccionar();
            return Json(db_Sucursal);
        }

        [HttpPost]
        public async Task<JsonResult> CreateSucursal([FromBody] SucursalViewModel sucursal)
        {
            SucursalViewModel db_Sucursal = await _sucursalRepository.CreateSucursal(sucursal);

            HttpContext.Session.Remove("ListEquipoRutina");
            HttpContext.Session.Remove("ListEquipoImagen");
            HttpContext.Session.Remove("ListEquipoTicket");

            return Json(db_Sucursal);
        }

        [HttpPost]
        public async Task<JsonResult> GetSucursalIdEmpresa([FromBody] SucursalViewModel sucursal)
        {
            List<SucursalViewModel> db_Sucursal = await _sucursalRepository.GetSucursalIdEmpresa(sucursal);
            return Json(db_Sucursal);
        }
    }
}
