using ASD.Areas.Administracion.Models;
using ASD.Areas.Mailing.Views;
using ASD.Areas.Persona.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Utilidades;
using Azure;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Remote;
using System.Security.Claims;

namespace ASD.Areas.Ticket.Controllers
{
    [Area("Ticket")]
    public class OrdenTrabajoController : Controller
    {
        private readonly ILogger<OrdenTrabajoController> _logger;
        private readonly IControlArchivoRepository _controlArchivoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IOrdenTrabajoRepository _ordenTrabajoRepository;
        private readonly IUsuarioFlujoRepository _usuarioFlujoRepository;
        private readonly IBitacoraRepository _bitacoraRepository;
        public OrdenTrabajoController(ILogger<OrdenTrabajoController> logger, IControlArchivoRepository controlArchivoRepository, ITicketRepository ticketRepository, IOrdenTrabajoRepository ordenTrabajoRepository, IUsuarioFlujoRepository usuarioFlujoRepository, IBitacoraRepository bitacoraRepository)
        {
            _logger = logger;
            _controlArchivoRepository = controlArchivoRepository;
            _ticketRepository = ticketRepository;
            _ordenTrabajoRepository = ordenTrabajoRepository;
            _usuarioFlujoRepository = usuarioFlujoRepository;
            _bitacoraRepository = bitacoraRepository;
        }

        [HttpPost]
        public async Task<JsonResult> UpdateOrdenTrabajo_ActualizarEstatus([FromBody] OrdenTrabajoViewModel ordenTrabajo)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                usuario.Id = Convert.ToInt32(claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            }
            ordenTrabajo.Usuario = usuario;
            OrdenTrabajoViewModel db_OrdenTrabajo = await _ordenTrabajoRepository.UpdateOrdenTrabajo_ActualizarEstatus(ordenTrabajo);

            if(ordenTrabajo.Notificar == 1)
            {
                // lista de email, notifica a priera etapa
                List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(ordenTrabajo.Ticket, "Cliente");
                // informacion del ticket
                CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ordenTrabajo.Ticket);
                CBitacoraViewModel db_bitaora = await _bitacoraRepository.GetBitacora_UltimoMovimiento_IdTicket(ordenTrabajo.Ticket);
                // Ultimo movimiento 

                Mailing.Formato.EstatusOrdenTrabajo estatusOrdenTrabjo = new Mailing.Formato.EstatusOrdenTrabajo();

                foreach (EmailViewModel email in Emails)
                {

                    string html = estatusOrdenTrabjo.HtmlEstatusOrdenTrabajo(email, db_ticket, db_bitaora);

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
                        Asunto = "Cambio de estatus a orden de trabajo, folio " + db_ticket.Folio.Folio,    // Asunto del correo
                                                                                                            // Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }

            }

            return Json(db_OrdenTrabajo);
        }

        [HttpPost]
        public virtual async Task<JsonResult> MostrarOrdenTrabajo([FromBody]  TicketViewModel ticket)
        {
            OrdenTrabajoViewModel ordenTrabajo = await _ordenTrabajoRepository.GetOrdenTrabajo_Seleccionar_IdTicket(ticket);
            return Json(ordenTrabajo);
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file, int Id)
        {
            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Id;
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string IdUser;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUser = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUser);
            }

            // Obtiene el nombre completo del archivo
            string fileName = file.FileName;

            // Separa el nombre del archivo de la extensión
            string[] parts = fileName.Split('.');

            // Obtiene la extensión del archivo
            string extension = parts[parts.Length - 1];

            string NombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + "." + extension;

            string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\ORDENTRABAJO\\";

            if (!Directory.Exists(directorioBase))
            {
                // Crea el directorio
                Directory.CreateDirectory(directorioBase);
            }

            var filePath = Path.Combine(directorioBase, NombreArchivo);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Position = 0; // Regresar al inicio del stream

                // Registra la orden de trabajo al ticket - con estatus nuevo.
                OrdenTrabajoViewModel ordenTrabajo = new OrdenTrabajoViewModel();
                ordenTrabajo.Ticket = ticket;
                ordenTrabajo.Usuario = usuario;
                ordenTrabajo.NmOriginal = file.FileName;
                ordenTrabajo.NmArchivo = NombreArchivo;

                OrdenTrabajoViewModel dbordenTrabajo = await _ordenTrabajoRepository.CreateOrdenTrabajo(ordenTrabajo);

                if (dbordenTrabajo.Id > 0)
                {
                    await _ordenTrabajoRepository.CreateOrdenTrabajo_ProcesarEtapa(ordenTrabajo);
                }

                Response.Headers.Add("Content-Length", stream.Length.ToString());
                //Response.Headers.Add("Name", file.FileName);
                Response.Headers.Add("Estatus", dbordenTrabajo.Id.ToString());
                await stream.CopyToAsync(Response.Body);

                
            }

            return Ok();
        }

        [HttpPost]
        public async Task<JsonResult> Notificacion_NuevaOrdenTrabajo([FromBody] TicketViewModel ticket)
        {

            List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(ticket, "Recepción");
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            Mailing.Formato.NuevaOrdenTrabajo nuevaOrdenTrabajo = new Mailing.Formato.NuevaOrdenTrabajo();

            foreach(EmailViewModel email in Emails) {
                var protocol = Request.Scheme;
                var host = Request.Host.Value;

                db_ticket.Direccion = protocol + "://" + host + "";

                string html = nuevaOrdenTrabajo.HtmlOrdenTrabajo(email, db_ticket);

                // New EMail    
                MailCreate correoNuevo = new MailCreate()
                {
                    Token = string.Empty,                                                           // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                    AplicacionUserId = 1,                                                           // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                    Aplicacion = "ASD",                                                             // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                    Origen = "ASAE SERVICE DESK (ASD)",                                             // nombre como se desea que aparezca el correo
                    Destinatario = email.Email,                                                     // Correo del destinatario
                    DestinatarioCC = string.Empty,                                                  // Agregar un destinatario con copia
                    DestinatarioCCO = string.Empty,                                                 // Agregar un destinatario con copia oculta
                    Asunto = "Nueva orden de trabajo registrada folio " + db_ticket.Folio.Folio,    // Asunto del correo
                    //Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                      // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                    Contenido = html,
                    fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                };

                Task<Mail> envio = Email.SendAsync(correoNuevo);
            }

            return Json(ticket);
        }

    }
}
