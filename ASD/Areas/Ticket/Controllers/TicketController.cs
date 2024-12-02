 using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Operacion.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.TicketAtencionMovil;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OpenQA.Selenium.DevTools.V118.Network;
using System.Security.Claims;
using static ASD.Repository.Interface.Administracion.IUrlRepository;
using System.Security.Policy;
using ASD.Areas.Mailing.Views;
using ASD.Areas.Persona.Models;
using OpenQA.Selenium.DevTools.V118.Profiler;
using System.Collections.Generic;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IFlujoRepository _flujoRepository;
        private readonly ICat_PrioridadRepository _cat_prioridadRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ICat_AsignacionEmpresaRepository _cat_AsignacionEmpresaRepository;
        private readonly ICuadrillaRepository _cuadrillaRepository;
        private readonly ICat_EstatusTicketRepository _cat_EstatusTicketRepository;
        private readonly ICat_EstatusOrdenTrabajoRepository _cat_EstatusOrdenTrabajoRepository;
        private readonly ICat_EstadoRepository _cat_EstadoRepository;
        private readonly ICat_TipoSucursalRepository _cat_TipoSucursalRepository;
        private readonly ICat_TipoEquipoRepository _cat_TipoEquipoRepository;
        private readonly ICat_EstatusEquipoRepository _cat_EstatusEquipoRepository;
        private readonly ITicketTipoServicioRepository _ticketTipoServicioRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;
        private readonly ITicketEquipoRutinaRepository _ticketEquipoRutinaRepository;
        private readonly ITicketEquipoImagenRepository _ticketEquipoImagenRepository;
        private readonly IBitacoraRepository _bitacoraRepository;
        private readonly ICat_EstatusFechaAtencionRepository _cat_EstatusFechaAtencion;
        private readonly ICuadrillaUsuarioRepository _cuadrillaUsuarioRepository;
        private readonly ITicketEtapaRepository _ticketEtapaRepository;
        private readonly ISucursalVideoRepository _sucursalVideoRepository;
        private readonly ISucursalImgRepository _sucursalImgRepository;
        private readonly IArchivoRepository _ArchivoRepository;
        private readonly IFirmaElectronicaRepository _firmaElectronicaRepository;
        private readonly ICierreClienteRepository _CierreClienteRepository;
        private readonly IDTiempoAtencionRepository _DTiempoAtencionRepository;
        private readonly IUsuarioFlujoRepository _usuarioFlujoRepository;
        private readonly IEquipoUsuarioRepository _equipoUsuarioRepository;


        public TicketController(ILogger<TicketController> logger, IFlujoRepository flujoRepository, ICat_PrioridadRepository cat_PrioridadRepository, ISucursalRepository sucursalRepository, ITicketRepository ticketRepository, ICat_AsignacionEmpresaRepository cat_AsignacionEmpresaRepository, ICuadrillaRepository cuadrillaRepository, ICat_EstatusTicketRepository cat_EstatusTicketRepository, ICat_EstatusOrdenTrabajoRepository cat_EstatusOrdenTrabajoRepository, ICat_EstadoRepository cat_EstadoRepository, 
            ICat_TipoSucursalRepository cat_TipoSucursalRepository, ICat_TipoEquipoRepository cat_TipoEquipoRepository, ICat_EstatusEquipoRepository cat_EstatusEquipoRepository, ITicketTipoServicioRepository ticketTipoServicioRepository, ITicketEquipoRepository ticketEquipoRepository, ITicketEquipoRutinaRepository ticketEquipoRutinaRepository, ITicketEquipoImagenRepository ticketEquipoImagenRepository, IBitacoraRepository bitacoraRepository, ICat_EstatusFechaAtencionRepository cat_EstatusFechaAtencionRepository,
            ICuadrillaUsuarioRepository cuadrillaUsuarioRepository, ITicketEtapaRepository ticketEtapaRepository, ISucursalVideoRepository sucursalVideoRepository, ISucursalImgRepository sucursalImgRepository, IArchivoRepository archivoRepository, IFirmaElectronicaRepository firmaElectronicaRepository, ICierreClienteRepository cierreClienteRepository, IDTiempoAtencionRepository dTiempoAtencionRepository, IUsuarioFlujoRepository usuarioFlujoRepository, IEquipoUsuarioRepository equipoUsuarioRepository)
        {
            _logger = logger;
            _flujoRepository = flujoRepository;
            _cat_prioridadRepository = cat_PrioridadRepository;
            _sucursalRepository = sucursalRepository;
            _ticketRepository = ticketRepository;
            _cat_AsignacionEmpresaRepository = cat_AsignacionEmpresaRepository;
            _cuadrillaRepository = cuadrillaRepository;
            _cat_EstatusTicketRepository = cat_EstatusTicketRepository;
            _cat_EstatusOrdenTrabajoRepository = cat_EstatusOrdenTrabajoRepository;
            _cat_EstadoRepository = cat_EstadoRepository;
            _cat_TipoSucursalRepository = cat_TipoSucursalRepository;
            _cat_TipoEquipoRepository = cat_TipoEquipoRepository;
            _cat_EstatusEquipoRepository = cat_EstatusEquipoRepository;
            _ticketTipoServicioRepository = ticketTipoServicioRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
            _ticketEquipoRutinaRepository = ticketEquipoRutinaRepository;
            _ticketEquipoImagenRepository = ticketEquipoImagenRepository;
            _bitacoraRepository = bitacoraRepository;
            _cat_EstatusFechaAtencion = cat_EstatusFechaAtencionRepository;
            _cuadrillaUsuarioRepository = cuadrillaUsuarioRepository;
            _ticketEtapaRepository = ticketEtapaRepository;
            _sucursalVideoRepository = sucursalVideoRepository;
            _sucursalImgRepository = sucursalImgRepository;
            _ArchivoRepository = archivoRepository;
            _firmaElectronicaRepository = firmaElectronicaRepository;
            _CierreClienteRepository = cierreClienteRepository;
            _DTiempoAtencionRepository = dTiempoAtencionRepository;
            _usuarioFlujoRepository = usuarioFlujoRepository;
            _equipoUsuarioRepository = equipoUsuarioRepository;
        }

        public virtual async Task<IActionResult> Operacion()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion(usuario);
            List<Cat_AsignacionEmpresaViewModel> db_CatAsignacionEmpresa = await _cat_AsignacionEmpresaRepository.GetCat_AsignacionEmpresa_Seleccionar();
            List<CuadrillaViewModel> db_cuadrilla = await _cuadrillaRepository.GetCuadrilla_Seleccionar();
            List<Cat_EstatusTicketViewModel> db_CatEstatusTicket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();
            List<Cat_EstatusOrdenTrabajoViewModel> db_catEstatusOrdenTrabajo = await _cat_EstatusOrdenTrabajoRepository.GetCat_EstatusOrdenTrabajo_Seleccionar();

            if (db_tickets!= null)
            {
                for (int i = 0; i < db_tickets.Count; i++)
                {
                    if (db_tickets[i].Cuadrilla != null)
                    {
                        db_tickets[i].ListaCuadrilla = await _cuadrillaUsuarioRepository.GetCuadrillaUsuario_Seleccionar_IdCuadrilla(db_tickets[i].Cuadrilla);
                    }
                }
            }
            
            ViewBag.Tickets = db_tickets;
            ViewBag.CatAsigacionEmpresa = db_CatAsignacionEmpresa;
            ViewBag.Cuadrillas = db_cuadrilla;
            ViewBag.CatEstatusTicket = db_CatEstatusTicket;
            ViewBag.CatPrioridad = db_CatPrioridad;
            ViewBag.CatEstatusOrdenTrabajo = db_catEstatusOrdenTrabajo;

            return View();
        }

        public virtual async Task<IActionResult> OperacionSupervicionNoOrdenTrabajo()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion(usuario);
            List<Cat_AsignacionEmpresaViewModel> db_CatAsignacionEmpresa = await _cat_AsignacionEmpresaRepository.GetCat_AsignacionEmpresa_Seleccionar();
            List<CuadrillaViewModel> db_cuadrilla = await _cuadrillaRepository.GetCuadrilla_Seleccionar();
            List<Cat_EstatusTicketViewModel> db_CatEstatusTicket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();

            if (db_tickets != null)
            {
                for (int i = 0; i < db_tickets.Count; i++)
                {
                    if (db_tickets[i].Cuadrilla != null)
                    {
                        db_tickets[i].ListaCuadrilla = await _cuadrillaUsuarioRepository.GetCuadrillaUsuario_Seleccionar_IdCuadrilla(db_tickets[i].Cuadrilla);
                    }
                }
            }

            ViewBag.Tickets = db_tickets;
            ViewBag.CatAsigacionEmpresa = db_CatAsignacionEmpresa;
            ViewBag.Cuadrillas = db_cuadrilla;
            ViewBag.CatEstatusTicket = db_CatEstatusTicket;
            ViewBag.CatPrioridad = db_CatPrioridad;

            return View();
        }

        public virtual async Task<IActionResult> OperacionOrdenTrabajo()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_OperacionOrdenTrabajo(usuario);
            List<Cat_EstatusTicketViewModel> db_CatEstatusTicket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_Seleccionar();
            List<Cat_EstatusOrdenTrabajoViewModel> db_catEstatusOrdenTrabajo = await _cat_EstatusOrdenTrabajoRepository.GetCat_EstatusOrdenTrabajo_Seleccionar();

            ViewBag.Tickets = db_tickets;
            ViewBag.CatEstatusTicket = db_CatEstatusTicket;
            ViewBag.CatEstatusOrdenTrabajo = db_catEstatusOrdenTrabajo;

            return View();
        }

        public virtual async Task<IActionResult> OperacionNoOrdenTrabajo()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion(usuario);
            List<Cat_EstatusTicketViewModel> db_CatEstatusTicket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_Seleccionar();

            ViewBag.Tickets = db_tickets;
            ViewBag.CatEstatusTicket = db_CatEstatusTicket;

            return View();
        }

        public virtual async Task<IActionResult> OperacionUsuario()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion_Usuario(usuario);

            ViewBag.Tickets = db_tickets;

            return View();
        }

        public virtual async Task<IActionResult> AtendidosUsuario()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

			List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_AtendidosOperador(usuario);
			ViewBag.Tickets = db_tickets;
			return View();
		}

        public virtual async Task<IActionResult> ValidadoCliente()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_ValidadoCliente(usuario);
            ViewBag.Tickets = db_tickets;
            return View();
        }

        public virtual async Task<IActionResult> ValidadoSupervisor()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_ValidadoSupervisor(usuario);
            ViewBag.Tickets = db_tickets;

            return View();
        }

        public virtual async Task<IActionResult> MisTickets()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Seleccionar_IdUsuario(usuario);
            ViewBag.Tickets = db_tickets;

            return View();
        }
        
        public virtual async Task<IActionResult> Catalogos()
        {
            List<Cat_AsignacionEmpresaViewModel> db_CatAsignacionEmpresa = await _cat_AsignacionEmpresaRepository.GetCat_AsignacionEmpresa_Seleccionar();
            ViewBag.AsignacionEmpresa = db_CatAsignacionEmpresa;

            List<Cat_EstatusFechaAtencionViewModel> db_Cat_EstatusFechaAtencion = await _cat_EstatusFechaAtencion.GetCat_EstatusFechaAtencion();
            ViewBag.EstatusFechaAtencion = db_Cat_EstatusFechaAtencion;

            List<Cat_EstatusOrdenTrabajoViewModel> db_Cat_EstatusOrdenTrabajo = await _cat_EstatusOrdenTrabajoRepository.GetCat_EstatusOrdenTrabajo_Seleccionar();
            ViewBag.OrdenTrabajo = db_Cat_EstatusOrdenTrabajo;

            List<Cat_PrioridadViewModel> db_Cat_Prioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();
            ViewBag.Prioridad = db_Cat_Prioridad;

            return View();
        }

        public virtual async Task<IActionResult> ReasignarTickets()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if(claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CuadrillaViewModel> db_cuadrilla = await _cuadrillaRepository.GetCuadrilla_Seleccionar();
            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnReasignacion(usuario);

            if (db_tickets != null)
            {
                for (int i = 0; i < db_tickets.Count; i++)
                {
                    if (db_tickets[i].Cuadrilla != null)
                    {
                        db_tickets[i].ListaCuadrilla = await _cuadrillaUsuarioRepository.GetCuadrillaUsuario_Seleccionar_IdCuadrilla(db_tickets[i].Cuadrilla);
                    }
                }
            }

            ViewBag.Cuadrillas = db_cuadrilla;
            ViewBag.Tickets = db_tickets;

            return View();
        }



        public virtual async Task<IActionResult> Reportes()
        {
            List<TicketReporteViewmodel> R = await _ticketRepository.ObtenerTicketsPorMesYAnio();
            List<TicketReporteViewmodel> year = await _ticketRepository.ObtenerTicketsAnio();
            ViewBag.TicketsData = R;
            ViewBag.Year = year;
            return View();
        }




        [HttpGet()]
        public virtual async Task<IActionResult> TicketEstado([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            Cat_EstadoViewModel CatEstado = new Cat_EstadoViewModel();
            CatEstado.Id = Convert.ToInt32(IdN);

            UsuarioViewModel usuario = new UsuarioViewModel();
            string IdUsuario;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUsuario);
            }


            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Tickets_Seleccionar_Estado(usuario, CatEstado);
            ViewBag.Tickets = db_tickets;
            return View();
        }
        //PRIMERA PRUEBA DE DESENCRIPTADO
        [HttpGet()]
        public virtual async Task<IActionResult> Detalle([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);

            // Obtiene el protocolo (http o https)
            var protocol = Request.Scheme;

            // Obtiene el host (www.ejemplo.com)
            var host = Request.Host.Value;

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);
            List<CBitacoraViewModel> dbBitacora = await _bitacoraRepository.GetBitacora_Seleccionar_IdTicket(ticket);
            List<TicketEtapaViewModel> dbTicketEtapas = await _ticketEtapaRepository.GetTicketEtapa_Medidor(ticket);
            List<ArchivoViewModel> db_Archivos = await _ArchivoRepository.GetArchivo_Seleccionar_IdTicket(ticket);
            List<CTicketEtapa2ViewModel> db_Etapas = await _ticketEtapaRepository.GetTicketEtapa_Consualta_IdTicket(ticket);
            List<DTiempoAtencionViewModel> db_TiempoAtencion = await _DTiempoAtencionRepository.GetDhasbord_Tiempos_Atencion_Ticket(ticket);

            ViewBag.Ticket = db_ticket;
            ViewBag.Equipos = dbEquipos;
            ViewBag.Bitacora = dbBitacora;
            ViewBag.Etapas = dbTicketEtapas;
            ViewBag.Archivos = db_Archivos;
            ViewBag.TicketEtapa = db_Etapas;
            ViewBag.TiempoAtencion = db_TiempoAtencion;
            return View();
        }

        public virtual async Task<IActionResult> Crear()
        {
            HttpContext.Session.Remove("ListEquipoRutina");
            HttpContext.Session.Remove("ListEquipoImagen");
            HttpContext.Session.Remove("ListEquipoTicket");

            List<FlujoViewModel> db_Flujos = await _flujoRepository.GetFlujo_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();
            List<SucursalViewModel> db_Sucursal = await _sucursalRepository.GetSucursal_Seleccionar();
            List<Cat_EstadoViewModel> db_CatEstados = await _cat_EstadoRepository.GetCat_Estado_Seleccionar();
            List<Cat_TipoSucursalViewModel> db_TipoSucursal = await _cat_TipoSucursalRepository.GetCat_TipoSucursal_Seleccionar();
            List<Cat_TipoEquipoViewModel> db_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            List<Cat_EstatusEquipoViewModel> db_EstatusEquipo = await _cat_EstatusEquipoRepository.GetCat_EstatusEquipo_Seleccionar();


            ViewBag.Flujos = db_Flujos;
            ViewBag.CatPrioridad = db_CatPrioridad;
            ViewBag.Sucursal = db_Sucursal;
            ViewBag.CatEstados = db_CatEstados;
            ViewBag.CatTipoSucursal = db_TipoSucursal;
            ViewBag.CatTipoEquipo = db_TipoEquipo;
            ViewBag.CatEstatusEquipo = db_EstatusEquipo;

            return View();
        }

        public virtual async Task<IActionResult> CrearNoOrdenTrabajo()
        {
            HttpContext.Session.Remove("ListEquipoRutina");
            HttpContext.Session.Remove("ListEquipoImagen");
            HttpContext.Session.Remove("ListEquipoTicket");

            List<FlujoViewModel> db_Flujos = await _flujoRepository.GetFlujo_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();
            List<SucursalViewModel> db_Sucursal = await _sucursalRepository.GetSucursal_Seleccionar();
            List<Cat_EstadoViewModel> db_CatEstados = await _cat_EstadoRepository.GetCat_Estado_Seleccionar();
            List<Cat_TipoSucursalViewModel> db_TipoSucursal = await _cat_TipoSucursalRepository.GetCat_TipoSucursal_Seleccionar();
            List<Cat_TipoEquipoViewModel> db_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            List<Cat_EstatusEquipoViewModel> db_EstatusEquipo = await _cat_EstatusEquipoRepository.GetCat_EstatusEquipo_Seleccionar();


            ViewBag.Flujos = db_Flujos;
            ViewBag.CatPrioridad = db_CatPrioridad;
            ViewBag.Sucursal = db_Sucursal;
            ViewBag.CatEstados = db_CatEstados;
            ViewBag.CatTipoSucursal = db_TipoSucursal;
            ViewBag.CatTipoEquipo = db_TipoEquipo;
            ViewBag.CatEstatusEquipo = db_EstatusEquipo;

            return View();
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Editar([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);
            var protocol = Request.Scheme;
            var host = Request.Host.Value;

            CierreClienteViewModel CierreCliente = new CierreClienteViewModel();
            CierreCliente.IdTicket = Convert.ToInt32(IdN);

            List<FlujoViewModel> db_Flujos = await _flujoRepository.GetFlujo_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            InformacionAdicionalViewModel R = await _firmaElectronicaRepository.GetSignature(ticket);
            CierreClienteViewModel RC = await _CierreClienteRepository.Get_CierreCliente(CierreCliente);


            ViewBag.Flujos = db_Flujos;
            ViewBag.CatPrioridad = db_CatPrioridad;
            ViewBag.Ticket = db_ticket;
            ViewBag.Info = R;
            ViewBag.InfoC = RC;

            return View();
        }






      

        [HttpPost]
        public async Task<JsonResult> CreateTicket([FromBody] CreateTicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            CreateTicketViewModel db_Ticket = new CreateTicketViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            List<EquipoViewModel> sesion_equipo = new List<EquipoViewModel>();
            string Id;

            if (HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket") != null)
            {
                sesion_equipo = HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket");
            }

            if(sesion_equipo.Count > 0)
            {
                if (claimuser.Identity.IsAuthenticated)
                {
                    Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                    usuario.Id = Convert.ToInt32(Id);
                }

                ticket.Ticket.Usuario = usuario;
                // CREACION DEL TICKET
                db_Ticket = await _ticketRepository.CreateTicket(ticket);

                // REGISTRO DEL SERVICIO TicketTipoServicio
                if(ticket.TicketTipoServicio.Cat_TipoServicio.Id > 0)
                {
                    ticket.TicketTipoServicio.Ticket = db_Ticket.Ticket;
                    _ticketTipoServicioRepository.CreateTicketTipoServicio(ticket.TicketTipoServicio);
                }

                // REGISTRO DE LOS EQUIPOS TicketEquipo - Check list
                foreach (EquipoViewModel equipo in sesion_equipo)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Equipo = equipo;
                    ticketEquipo.Ticket = db_Ticket.Ticket;

                    _ticketEquipoRepository.CreateTicketEquipo(ticketEquipo);
                }


                db_Ticket.Id = 1;
            }
            else
            {
                db_Ticket.Id = 0;
            }

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicketNoOrdenTrabajo([FromBody] CreateTicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            CreateTicketViewModel db_Ticket = new CreateTicketViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            List<EquipoViewModel> sesion_equipo = new List<EquipoViewModel>();
            string Id;

            if (HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket") != null)
            {
                sesion_equipo = HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket");
            }

            if (sesion_equipo.Count > 0)
            {
                if (claimuser.Identity.IsAuthenticated)
                {
                    Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                    usuario.Id = Convert.ToInt32(Id);
                }

                ticket.Ticket.Usuario = usuario;
                // CREACION DEL TICKET
                db_Ticket = await _ticketRepository.CreateTicket(ticket);

                // REGISTRO DEL SERVICIO TicketTipoServicio
                if (ticket.TicketTipoServicio.Cat_TipoServicio.Id > 0)
                {
                    ticket.TicketTipoServicio.Ticket = db_Ticket.Ticket;
                    _ticketTipoServicioRepository.CreateTicketTipoServicio(ticket.TicketTipoServicio);
                }

                // REGISTRO DE LOS EQUIPOS TicketEquipo - Check list
                foreach (EquipoViewModel equipo in sesion_equipo)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Equipo = equipo;
                    ticketEquipo.Ticket = db_Ticket.Ticket;

                    _ticketEquipoRepository.CreateTicketEquipo(ticketEquipo);
                }

                // procesar a siguiente etapa, y notificar por correo 
                db_Ticket.Ticket.Usuario = usuario;
                // Procesar ticket
                TicketViewModel db_Procesar = await _ticketRepository.UpdateTicketProcesar_Cliente(db_Ticket.Ticket);
                // Notificar correo
                // lista de email, notifica a priera etapa
                List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(db_Ticket.Ticket, "Recepción");
                // informacion del ticket
                CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(db_Ticket.Ticket);

                Mailing.Formato.NuevoTicket FormatoNuevoticket = new Mailing.Formato.NuevoTicket();

                foreach (EmailViewModel email in Emails)
                {
                    var protocol = Request.Scheme;
                    var host = Request.Host.Value;

                    db_ticket.Direccion = protocol + "://" + host + "";

                    string html = FormatoNuevoticket.HtmlNuevoTicket(email, db_ticket);

                    // New EMail    
                    MailCreate correoNuevo = new MailCreate()
                    {
                        Token = string.Empty,                                                               // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                        AplicacionUserId = 1,                                                               // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                        Aplicacion = "ASD",                                                                 // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                        Origen = "ASAE SERVICE DESK (ASD)",                                                 // nombre como se desea que aparezca el correo
                        Destinatario = email.Email,                                                         // Correo del destinatario
                        DestinatarioCC = string.Empty,                                                      // Agregar un destinatario con copia
                        DestinatarioCCO = string.Empty,                                                     // Agregar un destinatario con copia oculta
                        Asunto = "Nuevo ticket registrado, folio " + db_ticket.Folio.Folio,              // Asunto del correo
                                                                                                            // Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }

                db_Ticket.Id = 1;
            }
            else
            {
                db_Ticket.Id = 0;
            }

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> GetTicket([FromBody] TicketViewModel ticket)
        {
            CTicketViewModel db_Ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTicket_ActualizarEstatus([FromBody] TicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                usuario.Id = Convert.ToInt32(claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            }
            ticket.Usuario = usuario;
            TicketViewModel db_Ticket = await _ticketRepository.UpdateTicket_ActualizarEstatus(ticket);

            return Json(db_Ticket);
        }

        public virtual async Task<IActionResult> Estatus()
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<CountTicketViewModel> R_Ticket = await _cat_EstatusTicketRepository.Cat_EstatusTicket_SeleccionarConteo(usuario);

            ViewBag.Estatus = R_Ticket;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> DeleteStatusTicket([FromBody] Cat_EstatusTicketViewModel Cat_EstatusTicket)
        {
            Cat_EstatusTicketViewModel R_Estatus = await _cat_EstatusTicketRepository.DeleteStatusTicket(Cat_EstatusTicket);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateStatusTicket([FromBody] Cat_EstatusTicketViewModel Cat_EstatusTicket)
        {
            Cat_EstatusTicketViewModel R_Estatus = await _cat_EstatusTicketRepository.UpdateStatusTicket(Cat_EstatusTicket);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> InsertStatusTicket([FromBody] Cat_EstatusTicketViewModel Cat_EstatusTicket)
        {
            Cat_EstatusTicketViewModel R_Estatus = await _cat_EstatusTicketRepository.InsertStatusTicket(Cat_EstatusTicket);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> GetTicket_Seleccionar_IdEstatus([FromBody] TicketViewModel ticket)
        {
            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Seleccionar_IdEstatus(ticket);
            return Json(db_tickets);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTicket_Procesar_SupervisorAsae([FromBody] TicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }
            ticket.Usuario = usuario;
            TicketViewModel db_tickets = await _ticketRepository.UpdateTicket_Procesar_SupervisorAsae(ticket);

            if(db_tickets.Id > 0)
            {
                // enviar correo
                if (ticket.Notificar == 1)
                {
                    // lista de email, notifica a priera etapa
                    List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(ticket, "Calidad");
                    // informacion del ticket
                    CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

                    Mailing.Formato.EstatusTicket estatusTicket = new Mailing.Formato.EstatusTicket();

                    foreach (EmailViewModel email in Emails)
                    {


                        var protocol = Request.Scheme;
                        var host = Request.Host.Value;

                        db_ticket.Direccion = protocol + "://" + host + "";

                        string html = estatusTicket.HtmlEstatusTicket(email, db_ticket);

                        // New EMail    
                        MailCreate correoNuevo = new MailCreate()
                        {
                            Token = string.Empty,                                                               // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                            AplicacionUserId = 1,                                                               // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                            Aplicacion = "ASD",                                                                 // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                            Origen = "ASAE SERVICE DESK (ASD)",                                                 // nombre como se desea que aparezca el correo
                            Destinatario = email.Email,                                                         // Correo del destinatario
                            DestinatarioCC = string.Empty,                                                      // Agregar un destinatario con copia
                            DestinatarioCCO = string.Empty,                                                     // Agregar un destinatario con copia oculta
                            Asunto = "Cambio de estatus a Ticket, folio " + db_ticket.Folio.Folio,              // Asunto del correo
                                                                                                                // Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                            Contenido = html,
                            fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                        };

                        Task<Mail> envio = Email.SendAsync(correoNuevo);
                    }

                }
            }
            
            return Json(db_tickets);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTicket_Procesar_SupervisorCliente([FromBody] TicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }
            ticket.Usuario = usuario;
            TicketViewModel db_tickets = await _ticketRepository.UpdateTicket_Procesar_SupervisorCliente(ticket);

            if (db_tickets.Id > 0)
            {
                // enviar correo
                if (ticket.Notificar == 1)
                {
                    // lista de email, notifica a priera etapa
                    List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(ticket, "Recepción");
                    // informacion del ticket
                    CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

                    Mailing.Formato.EstatusTicket estatusTicket = new Mailing.Formato.EstatusTicket();

                    foreach (EmailViewModel email in Emails)
                    {
                        var protocol = Request.Scheme;
                        var host = Request.Host.Value;

                        db_ticket.Direccion = protocol + "://" + host + "";

                        string html = estatusTicket.HtmlEstatusTicket(email, db_ticket);

                        // New EMail    
                        MailCreate correoNuevo = new MailCreate()
                        {
                            Token = string.Empty,                                                               // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                            AplicacionUserId = 1,                                                               // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                            Aplicacion = "ASD",                                                                 // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                            Origen = "ASAE SERVICE DESK (ASD)",                                                 // nombre como se desea que aparezca el correo
                            Destinatario = email.Email,                                                         // Correo del destinatario
                            DestinatarioCC = string.Empty,                                                      // Agregar un destinatario con copia
                            DestinatarioCCO = string.Empty,                                                     // Agregar un destinatario con copia oculta
                            Asunto = "Cambio de estatus a Ticket, folio " + db_ticket.Folio.Folio,              // Asunto del correo
                                                                                                                // Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                            Contenido = html,
                            fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                        };

                        Task<Mail> envio = Email.SendAsync(correoNuevo);
                    }

                }
            }

            return Json(db_tickets);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTicket([FromBody] TicketViewModel ticketViewModel)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                usuario.Id = Convert.ToInt32(claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            }
            ticketViewModel.Usuario = usuario;

            TicketViewModel R_Estatus = await _ticketRepository.UpdateTicket(ticketViewModel);
            return Json(R_Estatus);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTicket_Actualizar_IdPrioridad([FromBody] CreateTicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            ticket.Ticket.Usuario = usuario;
            TicketViewModel db_Ticket = await _ticketRepository.UpdateTicket_Actualizar_IdPrioridad(ticket.Ticket);

            return Json(db_Ticket);
        }





        [HttpPost]
        public async Task<JsonResult> ObtenerTotalTicketsPorEstatusAnio([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerTotalTicketsPorEstatusAnio(Model);

            return Json(db_Ticket);
        }
        [HttpPost]
        public async Task<JsonResult> ObtenerTotalTicketsPorFlujoAnio([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerTotalTicketsPorFlujoAnio(Model);

            return Json(db_Ticket);
        }
        [HttpPost]
        public async Task<JsonResult> ObtenerTicketsAtendidos([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerTicketsAtendidos(Model);

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerResumenFaltante([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerResumenFaltante(Model);

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerTicketIdflujoUser([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerTicketIdflujoUser(Model);

            return Json(db_Ticket);
        }


        [HttpPost]
        public async Task<JsonResult> GetTicketEstatusAnio([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.GetTicketEstatusAnio(Model);

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> GetTicketAnioMonth([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.GetTicketAnioMonth(Model);

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerTicketXservicio([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerTicketXservicio(Model);

            return Json(db_Ticket);
        }
        [HttpPost]
        public async Task<JsonResult> ObtenerTicketFinalizado([FromBody] TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel> db_Ticket = await _ticketRepository.ObtenerTicketFinalizado(Model);

            return Json(db_Ticket);
        }

        [HttpPost]
        public async Task<JsonResult> TicketFechaCorte([FromBody] TicketReporteViewmodel Model)
        {
            TicketReporteViewmodel db_Ticket = await _ticketRepository.TicketFechaCorte(Model);

            return Json(db_Ticket);
        }
    }
}
