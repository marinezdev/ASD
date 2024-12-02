using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Mailing.Formato;
using ASD.Areas.Mailing.Views;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Operacion.Models.Consulta;
using ASD.Areas.Persona.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Areas.TicketUnitario.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Persona;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.TicketAtencionMovil;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.TicketUnitario.Controllers
{
    [Authorize]
    [Area("TicketUnitario")]
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
        private readonly IPersonaRepository _personaRepository;

        public TicketController(ILogger<TicketController> logger, IFlujoRepository flujoRepository, ICat_PrioridadRepository cat_PrioridadRepository, ISucursalRepository sucursalRepository, ITicketRepository ticketRepository, ICat_AsignacionEmpresaRepository cat_AsignacionEmpresaRepository, ICuadrillaRepository cuadrillaRepository, ICat_EstatusTicketRepository cat_EstatusTicketRepository, ICat_EstatusOrdenTrabajoRepository cat_EstatusOrdenTrabajoRepository, ICat_EstadoRepository cat_EstadoRepository,
            ICat_TipoSucursalRepository cat_TipoSucursalRepository, ICat_TipoEquipoRepository cat_TipoEquipoRepository, ICat_EstatusEquipoRepository cat_EstatusEquipoRepository, ITicketTipoServicioRepository ticketTipoServicioRepository, ITicketEquipoRepository ticketEquipoRepository, ITicketEquipoRutinaRepository ticketEquipoRutinaRepository, ITicketEquipoImagenRepository ticketEquipoImagenRepository, IBitacoraRepository bitacoraRepository, ICat_EstatusFechaAtencionRepository cat_EstatusFechaAtencionRepository,
            ICuadrillaUsuarioRepository cuadrillaUsuarioRepository, ITicketEtapaRepository ticketEtapaRepository, ISucursalVideoRepository sucursalVideoRepository, ISucursalImgRepository sucursalImgRepository, IArchivoRepository archivoRepository, IFirmaElectronicaRepository firmaElectronicaRepository, ICierreClienteRepository cierreClienteRepository, IDTiempoAtencionRepository dTiempoAtencionRepository, IUsuarioFlujoRepository usuarioFlujoRepository, IEquipoUsuarioRepository equipoUsuarioRepository,
            IPersonaRepository personaRepository)
        {
            _logger = logger;
            _personaRepository = personaRepository;
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

        public virtual async Task<IActionResult> CrearTicketUsuer()
        {
            HttpContext.Session.Remove("ListEquipoTicket");
            HttpContext.Session.Remove("ListArchivosTicket");

            UsuarioViewModel usuario = new UsuarioViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            List<FlujoViewModel> db_Flujos = await _flujoRepository.GetFlujo_Seleccionar();
            List<Cat_PrioridadViewModel> db_CatPrioridad = await _cat_prioridadRepository.GetCat_Prioridad_Seleccionar();
            List<SucursalViewModel> db_Sucursal = await _sucursalRepository.GetSucursal_Seleccionar();
            List<Cat_EstadoViewModel> db_CatEstados = await _cat_EstadoRepository.GetCat_Estado_Seleccionar();
            List<Cat_TipoSucursalViewModel> db_TipoSucursal = await _cat_TipoSucursalRepository.GetCat_TipoSucursal_Seleccionar();
            List<Cat_TipoEquipoViewModel> db_TipoEquipo = await _cat_TipoEquipoRepository.GetCat_TipoEquipo_Seleccionar();
            List<Cat_EstatusEquipoViewModel> db_EstatusEquipo = await _cat_EstatusEquipoRepository.GetCat_EstatusEquipo_Seleccionar();
            List<EquipoViewModel> equipos = await _equipoUsuarioRepository.GetEquipoUsuario_Seleccionar_IdUsuario(usuario);

            ViewBag.Flujos = db_Flujos;
            ViewBag.CatPrioridad = db_CatPrioridad;
            ViewBag.Sucursal = db_Sucursal;
            ViewBag.CatEstados = db_CatEstados;
            ViewBag.CatTipoSucursal = db_TipoSucursal;
            ViewBag.CatTipoEquipo = db_TipoEquipo;
            ViewBag.CatEstatusEquipo = db_EstatusEquipo;
            ViewBag.EquipoUsuarios = equipos;

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

        [HttpGet()]
        public virtual async Task<IActionResult> Atender([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            List<ArchivoViewModel> db_Archivos = await _ArchivoRepository.GetArchivo_Seleccionar_IdTicket(ticket);

            ViewBag.Ticket = db_ticket;
            ViewBag.Archivos = db_Archivos;

            return View();
        }

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

            ViewBag.Data = Id;
            ViewBag.Ticket = db_ticket;
            ViewBag.Equipos = dbEquipos;
            ViewBag.Bitacora = dbBitacora;
            ViewBag.Etapas = dbTicketEtapas;
            ViewBag.Archivos = db_Archivos;
            ViewBag.TicketEtapa = db_Etapas;
            ViewBag.TiempoAtencion = db_TiempoAtencion;
            return View();
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Programar([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            List<CuadrillaViewModel> db_cuadrilla = await _cuadrillaRepository.GetCuadrilla_Seleccionar();
            List<CTicketEquipoViewModel> db_equipo = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);


            if (db_equipo != null && db_equipo.Any())
            {
                ViewBag.equipo = db_equipo.FirstOrDefault();
            }
            else
            {
                ViewBag.equipo = null;
            }

            ViewBag.Ticket = db_ticket;
            ViewBag.Cuadrillas = db_cuadrilla;
            return View();
        }


        [AllowAnonymous]
        [HttpGet()]
        public virtual async Task<IActionResult> Encuesta([FromQuery(Name = "cont")] string Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ReturnUrl"] = Url.Action("Encuesta", new { cont = Id });
                string loginUrl = Url.Action("Index", "Login", new { area = "Administracion" });
                return Redirect(loginUrl);
            }


            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);


            CierreClienteViewModel CierreCliente = new CierreClienteViewModel();
            CierreCliente.IdTicket = Convert.ToInt32(IdN);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            CierreClienteViewModel R = await _CierreClienteRepository.Get_CierreCliente(CierreCliente);

            ViewBag.Ticket = db_ticket;
            ViewBag.Info = R;

            if (db_ticket.Ticket.Cat_EstatusTicket.Id != 1)
            {
                return Redirect("/TicketUnitario/Ticket/MisTickets");
            }

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CreateTicket_Crear_Usuario([FromBody] CreateTicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            CreateTicketViewModel db_NTicket = new CreateTicketViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            ticket.Ticket.Usuario = usuario;

            // Creacion del ticket
            db_NTicket = await _ticketRepository.CreateTicket_Crear_Usuario(ticket);

            // Valida la creacion del ticket, informacion adicional despues de la creacion del ticket
            if (db_NTicket.Ticket.Id > 0)
            {
                CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(db_NTicket.Ticket);

                // archivos
                if (ticket.Archivo > 0)
                {
                    if (HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket") != null)
                    {
                        // Consualta la lista de archivos, para registrar
                        List<ArchivoViewModel> sesion_archivos = new List<ArchivoViewModel>();

                        // Ruta temporal
                        string DirectorioTemporal = "C:\\File\\ASD\\ARCHIVOS_TEMPORALES\\";
                        sesion_archivos = HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket");

                        // archivos registrados en memoria
                        if (sesion_archivos.Count > 0)
                        {
                            foreach (ArchivoViewModel archivo in sesion_archivos)
                            {
                                string sourcePath = DirectorioTemporal + archivo.NmArchivo;
                                string destinationPath = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\ARCHIVOS_ADICIONALES\\";

                                if (!Directory.Exists(destinationPath))
                                {
                                    // Crea el directorio
                                    Directory.CreateDirectory(destinationPath);
                                }

                                if (System.IO.File.Exists(sourcePath))
                                {
                                    try
                                    {
                                        string ruta = destinationPath + archivo.NmArchivo;
                                        System.IO.File.Move(sourcePath, ruta);
                                        archivo.Ticket = db_NTicket.Ticket;
                                        Console.WriteLine("File moved successfully!");
                                        ArchivoViewModel dbArchivo = await _ArchivoRepository.CreateArchivo(archivo);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error moving file: " + ex.Message);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Source file not found: {0}", sourcePath);
                                }
                            }
                        }
                    }
                }

                // equipo
                if (ticket.Equipo > 0)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Equipo = ticket.TicketEquipo.Equipo;
                    ticketEquipo.Ticket = db_ticket.Ticket;

                    _ticketEquipoRepository.CreateTicketEquipo(ticketEquipo);
                }

                db_NTicket.Ticket.Usuario = usuario;
                // Procesar ticket
                TicketViewModel db_Procesar = await _ticketRepository.UpdateTicketProcesar_Cliente(db_NTicket.Ticket);

                // Notificar a lider de proyecto
                // lista de email, notifica a priera etapa
                List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(db_NTicket.Ticket, "Recepción");
                // informacion del ticket
                //CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(db_NTicket.Ticket);

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
                        Asunto = "Nuevo ticket registrado, folio " + db_ticket.Folio.Folio,                 // Asunto del correo
                                                                                                            // Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }
            }

            return Json(db_NTicket);
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
        public async Task<JsonResult> Ticket_Procesar_Atendido([FromBody] TicketAtenderViewModel ticket)
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

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            TicketAtenderViewModel R = await _ticketRepository.Ticket_Procesar_Atendido(ticket);
            CPersonaViewModel P = await _personaRepository.Persona_listar_IdTicket(ticket);

            db_ticket.TicketAtenderViewModel = ticket;


            var protocol = Request.Scheme;
            var host = Request.Host.Value;

            db_ticket.Direccion = protocol + "://" + host + "/TicketUnitario/Ticket/Encuesta?cont=" + db_ticket.TicketAtenderViewModel.ClaveEncriptada;

            string html = TicketAtendidoLiderArea.HtmlTicketAtendidoLiderArea(P, db_ticket);

            MailCreate correoNuevo = new MailCreate()
            {
                Token = string.Empty,                                                      
                AplicacionUserId = 1,                                                       
                Aplicacion = "ASD",                                                         
                Origen = "ASAE SERVICE DESK (ASD)",                                        
                Destinatario = P.Email.Email,
                DestinatarioCC = string.Empty,                                             
                DestinatarioCCO = string.Empty,                                             
                Asunto = "Ticket Atendido con Folio " + db_ticket.Folio.Folio,    
                Contenido = html,
                fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       
            };
            Task<Mail> envio = Email.SendAsync(correoNuevo);

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Ticket_Procesar_Cierre([FromBody] TicketAtenderViewModel ticket)
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

            TicketAtenderViewModel R = await _ticketRepository.Ticket_Procesar_Cierre(ticket);
   
            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Ticket_Procesar_NoAtendido([FromBody] TicketAtenderViewModel ticket)
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


            //DATOS del ticket
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            //DATOS DE LA PERSONA CLIENTE
            CPersonaViewModel P = await _personaRepository.Persona_listar_IdTicket(ticket);
            //DATOS DEL que atendio el ticket
            CPersonaViewModel AP = await _personaRepository.PersonaAtendio_listar_IdTicket(ticket);
            //Marcar como no atendido
            TicketAtenderViewModel R = await _ticketRepository.Ticket_Procesar_NoAtendido(ticket);

            db_ticket.TicketAtenderViewModel = ticket;
            var protocol = Request.Scheme;
            var host = Request.Host.Value;

            db_ticket.Direccion = protocol + "://" + host;

            string html = TicketNoAtendido.HtmlTicketNoAtendidoLiderArea(P,AP,db_ticket);

            MailCreate correoNuevo = new MailCreate()
            {
                Token = string.Empty,
                AplicacionUserId = 1,
                Aplicacion = "ASD",
                Origen = "ASAE SERVICE DESK (ASD)",
                Destinatario = AP.Email.Email,
                DestinatarioCC = string.Empty,
                DestinatarioCCO = string.Empty,
                Asunto = "Ticket Atendido con Folio " + db_ticket.Folio.Folio,
                Contenido = html,
                fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            };
            Task<Mail> envio = Email.SendAsync(correoNuevo);

            return Json(R);
        }
    }
}
