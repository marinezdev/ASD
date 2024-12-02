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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class NuevaFechaAtencionController : Controller
    {
        private readonly ILogger<NuevaFechaAtencionController> _logger;
        private readonly INuevaFechaAtencionRepository _NuevaFechaAtencionRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUsuarioFlujoRepository _UsuarioFlujoRepository;

        public NuevaFechaAtencionController(ILogger<NuevaFechaAtencionController> logger, INuevaFechaAtencionRepository nuevaFechaAtencionRepository, ITicketRepository ticketRepository, IUsuarioFlujoRepository usuarioFlujoRepository)
        {
            _logger = logger;
            _NuevaFechaAtencionRepository = nuevaFechaAtencionRepository;
            _ticketRepository = ticketRepository;
            _UsuarioFlujoRepository = usuarioFlujoRepository;
        }

        [HttpPost]
        public async Task<JsonResult> CreateNuevaFechaAtencion([FromBody] NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string IdUser;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUser = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUser);
            }
            nuevaFechaAtencion.Usuario = usuario;

            
            NuevaFechaAtencionViewModel dbNuevaFecha = await _NuevaFechaAtencionRepository.CreateNuevaFechaAtencion_Crear(nuevaFechaAtencion);
            

            if (nuevaFechaAtencion.Notificar == 1)
            {
                CNuevaFechaAtencionViewModel cNuevaFechaAtencion = await _NuevaFechaAtencionRepository.GetNuevaFechaAtencion_Seleccionar_IdTicket(nuevaFechaAtencion.Ticket);
                List<EmailViewModel> Emails = await _UsuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(nuevaFechaAtencion.Ticket, "Cliente");
                CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(nuevaFechaAtencion.Ticket);

                Mailing.Formato.CambioFechaAtencion cambioFechaAtencion = new Mailing.Formato.CambioFechaAtencion();

                foreach (EmailViewModel email in Emails)
                {

                    string html = cambioFechaAtencion.HtmlCambioFechaAtencion(email, db_ticket, cNuevaFechaAtencion);

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
                        Asunto = "Cambio Fecha de Atención Folio " + db_ticket.Folio.Folio,         // Asunto del correo
                                                                                                    // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }

            }

            return Json(dbNuevaFecha);
        }

        [HttpPost]
        public async Task<JsonResult> CreateNuevaFechaAtencionSuper([FromBody] NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string IdUser;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUser = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUser);
            }
            nuevaFechaAtencion.Usuario = usuario;


            NuevaFechaAtencionViewModel dbNuevaFecha = await _NuevaFechaAtencionRepository.CreateNuevaFechaAtencion_Crear_Super(nuevaFechaAtencion);


            if (nuevaFechaAtencion.Notificar == 1)
            {
                CNuevaFechaAtencionViewModel cNuevaFechaAtencion = await _NuevaFechaAtencionRepository.GetNuevaFechaAtencion_Seleccionar_IdTicket(nuevaFechaAtencion.Ticket);
                List<EmailViewModel> Emails = await _UsuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(nuevaFechaAtencion.Ticket, "Recepción");
                CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(nuevaFechaAtencion.Ticket);

                Mailing.Formato.CambioFechaAtencionSuper cambioFechaAtencionSuper = new Mailing.Formato.CambioFechaAtencionSuper();

                foreach (EmailViewModel email in Emails)
                {

                    string html = cambioFechaAtencionSuper.HtmlCambioFechaAtencionSuper(email, db_ticket, cNuevaFechaAtencion);

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
                        Asunto = "Cambio Fecha de Atención Folio " + db_ticket.Folio.Folio,         // Asunto del correo
                                                                                                    // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }

            }

            return Json(dbNuevaFecha);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateNuevaFechaAtencion_Aceptar([FromBody] NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string IdUser;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUser = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUser);
            }
            nuevaFechaAtencion.Usuario = usuario;
            NuevaFechaAtencionViewModel dbNuevaFecha = await _NuevaFechaAtencionRepository.UpdateNuevaFechaAtencion_Aceptar(nuevaFechaAtencion);


            if (nuevaFechaAtencion.Notificar == 1)
            {
                if(dbNuevaFecha.Id > 0)
                {
                    List<EmailViewModel> Emails = await _UsuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(nuevaFechaAtencion.Ticket, "Cliente");
                    CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(nuevaFechaAtencion.Ticket);

                    Mailing.Formato.ConfirmacionFechaAtencion ConfirmacionFechaAtencion = new Mailing.Formato.ConfirmacionFechaAtencion();

                    foreach (EmailViewModel email in Emails)
                    {
                        var protocol = Request.Scheme;
                        var host = Request.Host.Value;

                        db_ticket.Direccion = protocol + "://" + host + "";

                        string html = ConfirmacionFechaAtencion.HtmlConfirmacionFechaAtencion(email, db_ticket);

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
                            Asunto = "Confirmación de Aceptación de Fecha, Folio: " + db_ticket.Folio.Folio,         // Asunto del correo
                                                                                                        // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                            Contenido = html,
                            fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                        };

                        Task<Mail> envio = Email.SendAsync(correoNuevo);
                    }
                }
            }

            return Json(dbNuevaFecha);
        }

        [HttpPost]
        public async Task<JsonResult> CreateNuevaFechaAtencion_TicketUnitario([FromBody] NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string IdUser;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUser = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUser);
            }
            nuevaFechaAtencion.Usuario = usuario;


            NuevaFechaAtencionViewModel dbNuevaFecha = await _NuevaFechaAtencionRepository.CreateNuevaFechaAtencion_Crear_TicketUnitario(nuevaFechaAtencion);

            // Modulo de notificacion

            //if (nuevaFechaAtencion.Notificar == 1)
            //{
            //    CNuevaFechaAtencionViewModel cNuevaFechaAtencion = await _NuevaFechaAtencionRepository.GetNuevaFechaAtencion_Seleccionar_IdTicket(nuevaFechaAtencion.Ticket);
            //    List<EmailViewModel> Emails = await _UsuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(nuevaFechaAtencion.Ticket, "Cliente");
            //    CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(nuevaFechaAtencion.Ticket);

            //    Mailing.Formato.CambioFechaAtencion cambioFechaAtencion = new Mailing.Formato.CambioFechaAtencion();

            //    foreach (EmailViewModel email in Emails)
            //    {

            //        string html = cambioFechaAtencion.HtmlCambioFechaAtencion(email, db_ticket, cNuevaFechaAtencion);

            //        // New EMail    
            //        MailCreate correoNuevo = new MailCreate()
            //        {
            //            Token = string.Empty,                                                       // dejar vacío | es el identificador que regresará el API para la identificación del Correo
            //            AplicacionUserId = 1,                                                       // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
            //            Aplicacion = "ASD",                                                         // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
            //            Origen = "ASAE SERVICE DESK (ASD)",                                         // nombre como se desea que aparezca el correo
            //            Destinatario = email.Email,                                                 // Correo del destinatario
            //            DestinatarioCC = string.Empty,                                              // Agregar un destinatario con copia
            //            DestinatarioCCO = string.Empty,                                             // Agregar un destinatario con copia oculta
            //            Asunto = "Cambio Fecha de Atención Folio " + db_ticket.Folio.Folio,         // Asunto del correo
            //                                                                                        // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
            //            Contenido = html,
            //            fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
            //        };

            //        Task<Mail> envio = Email.SendAsync(correoNuevo);
            //    }

            //}

            return Json(dbNuevaFecha);
        }

    }
}
