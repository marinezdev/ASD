using ASD.Areas.Administracion.Controllers;
using ASD.Areas.Administracion.Models.Consulta;
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.Empresa;

namespace ASD.Areas.Inventario.Controllers
{
    [Authorize]
    [Area("Inventario")]
    public class EquipoController : Controller
    {
        private readonly ILogger<EquipoController> _logger;
        private readonly IEquipoRepository _equipoRepository;
        private readonly ICat_EquipoImagenRepository _cat_EquipoImagenRepository;
        private readonly ICat_EquipoRutinaRepository _cat_EquipoRutinaRepository;
        private readonly IEmpresaRepository _EmpresaRepository;
        private readonly ICat_TipoEquipoRepository _cat_TipoEquipoRepository;
        private readonly ICat_EstatusEquipoRepository _cat_EstatusEquipoRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;

        public EquipoController(ILogger<EquipoController> logger, IEquipoRepository equipoRepository, ICat_EquipoImagenRepository cat_EquipoImagenRepository, ICat_EquipoRutinaRepository cat_EquipoRutinaRepository, IEmpresaRepository EmpresaRepository, ICat_TipoEquipoRepository cat_TipoEquipoRepository, ICat_EstatusEquipoRepository cat_EstatusEquipoRepository, ITicketEquipoRepository ticketEquipoRepository)
        {
            _logger = logger;
            _equipoRepository = equipoRepository;
            _cat_EquipoImagenRepository = cat_EquipoImagenRepository;
            _cat_EquipoRutinaRepository = cat_EquipoRutinaRepository;
            _EmpresaRepository = EmpresaRepository;
            _cat_TipoEquipoRepository = cat_TipoEquipoRepository;
            _cat_EstatusEquipoRepository = cat_EstatusEquipoRepository;
            _ticketEquipoRepository = ticketEquipoRepository;

        }
        [HttpPost]
        public async Task<JsonResult> Equipo_Seleccionar_IdSucursal([FromBody] SucursalViewModel sucursal)
        {
            List<EquipoViewModel> db_Equipo = await _equipoRepository.GetEquipo_Seleccionar_IdSucursal(sucursal);
            return Json(db_Equipo);
        }


        [HttpPost]
        public async Task<JsonResult> CreateEquipo([FromBody] EquipoViewModel equipo)
        {
            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();
            List<Cat_EquipoRutinaViewModel> sesion_EquipoRutina = new List<Cat_EquipoRutinaViewModel>();
            EquipoViewModel db_Equipo = new EquipoViewModel();

            if (HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen") != null)
            {
                sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");
            }

            if (HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina") != null)
            {
                sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");
            }

            // VALIDAR SI CONTIENE IMAGENES
            if (sesion_EquipoImagen.Count > 0)
            {
                // VALIDAR SI CONTIENE RUTINA
                if (sesion_EquipoRutina.Count > 0)
                {
                    db_Equipo = await _equipoRepository.CreateEquipo(equipo);

                    if (db_Equipo.Id > 0)
                    {
                        sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");

                        foreach (Cat_EquipoImagenViewModel cat_EquipoImagen in sesion_EquipoImagen)
                        {
                            cat_EquipoImagen.Equipo = db_Equipo;
                            _cat_EquipoImagenRepository.CreateCat_EquipoImagen(cat_EquipoImagen);
                        }

                        sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");

                        foreach (Cat_EquipoRutinaViewModel cat_EquipoRutina in sesion_EquipoRutina)
                        {
                            cat_EquipoRutina.Equipo = db_Equipo;
                            _cat_EquipoRutinaRepository.CreateCat_EquipoRutina(cat_EquipoRutina);
                        }

                        HttpContext.Session.Remove("ListEquipoRutina");
                        HttpContext.Session.Remove("ListEquipoImagen");
                    }
                }
                else
                {
                    db_Equipo.Id = -3;
                }
            }
            else
            {
                db_Equipo.Id = -2;
            }

            return Json(db_Equipo);
        }




        [HttpPost]
        public async Task<JsonResult> AsignarRutinasIMG([FromBody] EquipoViewModel equipo)
        {
            List<Cat_EquipoImagenViewModel> sesion_EquipoImagen = new List<Cat_EquipoImagenViewModel>();
            List<Cat_EquipoRutinaViewModel> sesion_EquipoRutina = new List<Cat_EquipoRutinaViewModel>();
            EquipoViewModel db_Equipo = new EquipoViewModel();

            if (HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen") != null)
            {
                sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");
            }

            if (HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina") != null)
            {
                sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");
            }


            // VALIDAR SI CONTIENE IMAGENES
            if (sesion_EquipoImagen.Count > 0)
            {
                // VALIDAR SI CONTIENE RUTINA
                if (sesion_EquipoRutina.Count > 0)
                {
                    db_Equipo = equipo;
                    //db_Equipo = await _equipoRepository.CreateEquipo(equipo);

                    if (db_Equipo.Id > 0)
                    {
                        sesion_EquipoImagen = HttpContext.Session.Get<List<Cat_EquipoImagenViewModel>>("ListEquipoImagen");

                        foreach (Cat_EquipoImagenViewModel cat_EquipoImagen in sesion_EquipoImagen)
                        {
                            cat_EquipoImagen.Equipo = db_Equipo;
                            _cat_EquipoImagenRepository.CreateCat_EquipoImagen(cat_EquipoImagen);
                        }

                        sesion_EquipoRutina = HttpContext.Session.Get<List<Cat_EquipoRutinaViewModel>>("ListEquipoRutina");

                        foreach (Cat_EquipoRutinaViewModel cat_EquipoRutina in sesion_EquipoRutina)
                        {
                            cat_EquipoRutina.Equipo = db_Equipo;
                            _cat_EquipoRutinaRepository.CreateCat_EquipoRutina(cat_EquipoRutina);
                        }

                        HttpContext.Session.Remove("ListEquipoRutina");
                        HttpContext.Session.Remove("ListEquipoImagen");
                    }
                }
                else
                {
                    db_Equipo.Id = -3;
                }
            }
            else
            {
                db_Equipo.Id = -2;
            }


            _ticketEquipoRepository.TicketEquipo_CrearSD(equipo);


            return Json(db_Equipo);

        }



        public virtual async Task<IActionResult> EquipoLista()
        {
            List<EmpresaViewModel> db_Empresa = await _EmpresaRepository.GetEmpresa_Seleccionar();
            List<Cat_TipoEquipoViewModel> db_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            List<Cat_EstatusEquipoViewModel> db_EstatusEquipo = await _cat_EstatusEquipoRepository.GetCat_EstatusEquipo_Seleccionar();


            ViewBag.Empresa = db_Empresa;
            ViewBag.CatTipoEquipo = db_TipoEquipo;
            ViewBag.CatEstatusEquipo = db_EstatusEquipo;

            return View();
        }
    }
}
