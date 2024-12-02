
using ASD.Areas.Administracion.Models;
using ASD.Areas.Mailing.Formato;
using ASD.Areas.Mailing.Views;
using ASD.Areas.Persona.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Utilidades;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketCuadrillaController : Controller
    {
        private readonly ILogger<TicketCuadrillaController> _logger;
        private readonly ITicketCuadrillaRepository _ticketCuadrillaRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly INuevaFechaAtencionRepository _nuevaFechaAtencionRepository;
        private readonly IUsuarioFlujoRepository _usuarioFlujoRepository;
        public TicketCuadrillaController(ILogger<TicketCuadrillaController> logger, ITicketCuadrillaRepository ticketCuadrillaRepository, ITicketRepository ticketRepository, INuevaFechaAtencionRepository nuevaFechaAtencionRepository, IUsuarioFlujoRepository usuarioFlujoRepository)
        {
            _logger = logger;
            _ticketCuadrillaRepository = ticketCuadrillaRepository;
            _ticketRepository = ticketRepository;
            _nuevaFechaAtencionRepository = nuevaFechaAtencionRepository;
            _usuarioFlujoRepository = usuarioFlujoRepository;
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicketCuadrilla([FromBody] TicketCuadrillaViewModel ticketCuadrilla)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            ticketCuadrilla.Ticket.Usuario = usuario;
            TicketCuadrillaViewModel db_ticketCuadrilla = await _ticketCuadrillaRepository.CreateTicketCuadrilla(ticketCuadrilla);

            if (db_ticketCuadrilla.Id > 0)
            {
                await _ticketCuadrillaRepository.CreateTicketCuadrilla_ProcesarEtapa(ticketCuadrilla);
                await _ticketCuadrillaRepository.CreateTicketCuadrilla_AsignarEtapa(ticketCuadrilla);

                if (ticketCuadrilla.Notificar == 1)
                {
                    CTicketCuadrillaViewModel cTicketCuadrilla = await _ticketCuadrillaRepository.GetTicketCuadrilla_Seleccionar_IdTicket(ticketCuadrilla.Ticket);
                    CNuevaFechaAtencionViewModel cNuevaFechaAtencion = await _nuevaFechaAtencionRepository.GetNuevaFechaAtencion_Seleccionar_IdTicket(ticketCuadrilla.Ticket);
                    CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticketCuadrilla.Ticket);

                    Mailing.Formato.AsignacionIDC asignacionIDC = new Mailing.Formato.AsignacionIDC();

                    var protocol = Request.Scheme;
                    var host = Request.Host.Value;
                    db_ticket.Direccion = protocol + "://" + host + "";

                    string html = asignacionIDC.HtmlAsignacionIDC(cTicketCuadrilla, db_ticket, cNuevaFechaAtencion);

                    // New EMail    
                    MailCreate correoNuevo = new MailCreate()
                    {
                        Token = string.Empty,                                                       // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                        AplicacionUserId = 1,                                                       // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                        Aplicacion = "ASD",                                                         // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                        Origen = "ASAE SERVICE DESK (ASD)",                                         // nombre como se desea que aparezca el correo
                        Destinatario = cTicketCuadrilla.Email.Email,                                                 // Correo del destinatario
                        DestinatarioCC = string.Empty,                                              // Agregar un destinatario con copia
                        DestinatarioCCO = string.Empty,                                             // Agregar un destinatario con copia oculta
                        Asunto = "Asignación de ticket, Folio " + db_ticket.Folio.Folio,            // Asunto del correo
                                                                                                    // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")                   // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }
            }

            return Json(ticketCuadrilla);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateTicketCuadrilla([FromBody] TicketCuadrillaViewModel ticketCuadrilla)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            ticketCuadrilla.Ticket.Usuario = usuario;
            TicketCuadrillaViewModel db_ticketCuadrilla = await _ticketCuadrillaRepository.UpdateTicketCuadrilla(ticketCuadrilla);

            if(db_ticketCuadrilla.Id > 0)
            {
                if (ticketCuadrilla.Notificar == 1)
                {
                    CTicketCuadrillaViewModel cTicketCuadrilla = await _ticketCuadrillaRepository.GetTicketCuadrilla_Seleccionar_IdTicket(ticketCuadrilla.Ticket);
                    CNuevaFechaAtencionViewModel cNuevaFechaAtencion = await _nuevaFechaAtencionRepository.GetNuevaFechaAtencion_Seleccionar_IdTicket(ticketCuadrilla.Ticket);
                    CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticketCuadrilla.Ticket);

                    Mailing.Formato.AsignacionIDC asignacionIDC = new Mailing.Formato.AsignacionIDC();
                    var protocol = Request.Scheme;
                    var host = Request.Host.Value;
                    db_ticket.Direccion = protocol + "://" + host + "";

                    string html = asignacionIDC.HtmlAsignacionIDC(cTicketCuadrilla, db_ticket, cNuevaFechaAtencion);

                    // New EMail    
                    MailCreate correoNuevo = new MailCreate()
                    {
                        Token = string.Empty,                                                       // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                        AplicacionUserId = 1,                                                       // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                        Aplicacion = "ASD",                                                         // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                        Origen = "ASAE SERVICE DESK (ASD)",                                         // nombre como se desea que aparezca el correo
                        Destinatario = cTicketCuadrilla.Email.Email,                                                 // Correo del destinatario
                        DestinatarioCC = string.Empty,                                              // Agregar un destinatario con copia
                        DestinatarioCCO = string.Empty,                                             // Agregar un destinatario con copia oculta
                        Asunto = "Asignación de ticket, Folio " + db_ticket.Folio.Folio,            // Asunto del correo
                                                                                                    // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")                   // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }
            }
            
            return Json(ticketCuadrilla);
        }
        [HttpPost]
        public async Task<JsonResult> CreateTicketCuadrilla_AtenderTicket([FromBody] TicketCuadrillaViewModel ticketCuadrilla)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            ticketCuadrilla.Usuario = usuario;
            TicketCuadrillaViewModel db_ticketCuadrilla = await _ticketCuadrillaRepository.CreateTicketCuadrilla_AtenderTicket(ticketCuadrilla);

            return Json(db_ticketCuadrilla);
        }
        [HttpPost]
        public async Task<JsonResult> CreateTicketCuadrilla_FinalizarTicket([FromBody] TicketCuadrillaViewModel ticketCuadrilla)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            ticketCuadrilla.Usuario = usuario;
            TicketCuadrillaViewModel db_ticketCuadrilla = await _ticketCuadrillaRepository.CreateTicketCuadrilla_FinalizarTicket(ticketCuadrilla);

            // Envio de correo electronico
            if (db_ticketCuadrilla.Id > 0)
            {
                List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(ticketCuadrilla.Ticket, "Control");
                CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticketCuadrilla.Ticket);

                Mailing.Formato.TicketAtentidoIDC ticketAtentidoIDC = new Mailing.Formato.TicketAtentidoIDC();

                foreach (EmailViewModel email in Emails)
                {
                    var protocol = Request.Scheme;
                    var host = Request.Host.Value;

                    db_ticket.Direccion = protocol + "://" + host + "";

                    string html = ticketAtentidoIDC.HtmlTicketAtendidoIDC(email, db_ticket);

                    // New EMail    
                    MailCreate correoNuevo = new MailCreate()
                    {
                        Token = string.Empty,                                                       // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                        AplicacionUserId = 1,                                                       // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                        Aplicacion = "ASD",                                                         // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                        Origen = "ASAE SERVICE DESK (ASD)",                                         // nombre como se desea que aparezca el correo
                        Destinatario = email.Email,                                                 // Correo del destinatario
                        DestinatarioCC = string.Empty,                                              // Agregar un destinatario con copia
                        DestinatarioCCO = string.Empty,                                             // Agregar un destinatario con copia oculta
                        Asunto = "Solución al ticket Folio: " + db_ticket.Folio.Folio,              // Asunto del correo
                                                                                                    // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")                   // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }

            }



            return Json(db_ticketCuadrilla);
        }
    }
}
