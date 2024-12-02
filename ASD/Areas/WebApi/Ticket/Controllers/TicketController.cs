using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Operacion.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.WebApi.Dhasbord.Models;
using ASD.Repository.Interface.Dhasbord;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;
using ASD.Areas.WebApi.Ticket.Models;
using DTicketViewModel = ASD.Areas.WebApi.Ticket.Models.DTicketViewModel;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Administracion;
using ASD.Areas.Persona.Models;
using ASD.Areas.Mailing.Views;
using ASD.Repository.Utilidades;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;
        private readonly IBitacoraRepository _bitacoraRepository;
        private readonly ITicketEtapaRepository _ticketEtapaRepository;
        private readonly IArchivoRepository _ArchivoRepository;
        private readonly IDTiempoAtencionRepository _DTiempoAtencionRepository;
        private readonly IEquipoUsuarioRepository _equipoUsuarioRepository;
        private readonly IFlujoRepository _flujoRepository;
        private readonly IControlArchivoRepository _controlArchivoRepository;
        private readonly IUsuarioFlujoRepository _usuarioFlujoRepository;

        public TicketController(ILogger<TicketController> logger, ITicketRepository ticketRepository, ITicketEquipoRepository ticketEquipoRepository, IBitacoraRepository bitacoraRepository, ITicketEtapaRepository ticketEtapaRepository, IArchivoRepository archivoRepository, IDTiempoAtencionRepository dTiempoAtencionRepository, IFlujoRepository flujoRepository, IEquipoUsuarioRepository equipoUsuarioRepository, IControlArchivoRepository controlArchivoRepository,
            IUsuarioFlujoRepository usuarioFlujoRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
            _bitacoraRepository = bitacoraRepository;
            _ticketEtapaRepository = ticketEtapaRepository;
            _ArchivoRepository = archivoRepository;
            _DTiempoAtencionRepository = dTiempoAtencionRepository;
            _flujoRepository = flujoRepository;
            _equipoUsuarioRepository = equipoUsuarioRepository;
            _controlArchivoRepository = controlArchivoRepository;
            _usuarioFlujoRepository = usuarioFlujoRepository;
        }

        [HttpPost]
        [Route("MisTickets")]
        public async Task<dynamic> TicketMisTicketsAsync([FromBody] UsuarioViewModel usuario)
        {
            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Seleccionar_IdUsuario(usuario);
            return (db_tickets);
        }


        [HttpPost]
        [Route("TicketDetalle")]
        public async Task<dynamic> TicketDetalleAsync([FromBody] TicketViewModel ticket)
        {
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);
            List<CBitacoraViewModel> dbBitacora = await _bitacoraRepository.GetBitacora_Seleccionar_IdTicket(ticket);
            List<TicketEtapaViewModel> dbTicketEtapas = await _ticketEtapaRepository.GetTicketEtapa_Medidor(ticket);
            List<ArchivoViewModel> db_Archivos = await _ArchivoRepository.GetArchivo_Seleccionar_IdTicket(ticket);
            List<CTicketEtapa2ViewModel> db_Etapas = await _ticketEtapaRepository.GetTicketEtapa_Consualta_IdTicket(ticket);
            List<DTiempoAtencionViewModel> db_TiempoAtencion = await _DTiempoAtencionRepository.GetDhasbord_Tiempos_Atencion_Ticket(ticket);

            DTicketViewModel db_Ticket = new DTicketViewModel();
            db_Ticket.Ticket = db_ticket;
            db_Ticket.TicketEquipos = dbEquipos;
            db_Ticket.TicketBitacoras = dbBitacora;
            db_Ticket.TicketEtapas = dbTicketEtapas;
            db_Ticket.TicketArchivos = db_Archivos;
            db_Ticket.TicketEtapasTiempo = db_Etapas;
            db_Ticket.TicketTiempoAtencion = db_TiempoAtencion;

            return (db_Ticket);
        }


        [HttpPost]
        [Route("TicketUnitarioNuevo")]
        public async Task<dynamic> TicketUnitarioNuevoAsync([FromBody] UsuarioViewModel usuario)
        {
            List<FlujoViewModel> db_Flujos = await _flujoRepository.GetFlujo_Seleccionar();
            List<EquipoViewModel> db_equipos = await _equipoUsuarioRepository.GetEquipoUsuario_Seleccionar_IdUsuario(usuario);

            TicketUnitarioViewModel ticketUnitario = new TicketUnitarioViewModel();
            ticketUnitario.ListEquipo = db_equipos;
            ticketUnitario.ListFlujo = db_Flujos;

            return (ticketUnitario);
        }

        [HttpPost]
        [Route("CreateTicketUnitario")]
        public async Task<dynamic> CreateTicketAsync([FromBody] FullTicketViewModel Fullticket)
        {
            CreateTicketViewModel db_ticket = new CreateTicketViewModel();


            if (Fullticket.CreateTicket != null)
            {
                // Nuevo ticket
                db_ticket = await _ticketRepository.CreateTicket_Crear_Usuario(Fullticket.CreateTicket);

                // Almacenamiento de equipo
                if (Fullticket.CreateTicket.Equipo > 0)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Equipo = Fullticket.CreateTicket.TicketEquipo.Equipo;
                    ticketEquipo.Ticket = db_ticket.Ticket;

                    _ticketEquipoRepository.CreateTicketEquipo(ticketEquipo);
                }

                // Almacenamiento de imagenes
                if (Fullticket.CreateTicket.Archivo > 0)
                {
                    foreach (ArchivoViewModel archivo in Fullticket.Archivos)
                    {
                        // decodificar el archivo base 64
                        ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

                        byte[] bytesImagen = Convert.FromBase64String(archivo.NmArchivo);
                        string NombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + "." + archivo.Extencion;
                        string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\ARCHIVOS_ADICIONALES\\";
                        string rutaDestino = directorioBase + NombreArchivo;
                        if (!Directory.Exists(directorioBase))
                        {
                            // Crea el directorio
                            Directory.CreateDirectory(directorioBase);
                        }

                        // Wrap in a using block for proper file stream disposal
                        using (FileStream fs = new FileStream(rutaDestino, FileMode.Create))
                        {
                            fs.Write(bytesImagen, 0, bytesImagen.Length);
                        }

                        //if (Directory.Exists(rutaDestino))

                            archivo.Usuario = Fullticket.CreateTicket.Ticket.Usuario;
                            archivo.Ticket = db_ticket.Ticket;
                            archivo.NmArchivo = NombreArchivo;
                            //archivo.NmOriginal = archivo.NmOriginal;

                            ArchivoViewModel dbArchivo = await _ArchivoRepository.CreateArchivo(archivo);
                        //}

                    }
                }

                // Procesar y notificar.
                TicketViewModel NV_Ticket = new TicketViewModel();
                db_ticket.Ticket.Detalle = Fullticket.CreateTicket.Ticket.Detalle;
                db_ticket.Ticket.Usuario = Fullticket.CreateTicket.Ticket.Usuario;
                NV_Ticket = db_ticket.Ticket;

                TicketViewModel db_Procesar = await _ticketRepository.UpdateTicketProcesar_Cliente(NV_Ticket);

                // Notificar lider proyecto
                // lista de email, notifica a priera etapa
                List<EmailViewModel> Emails = await _usuarioFlujoRepository.GetUsuarioEtapa_Notificacion_Operacion(db_ticket.Ticket, "Recepción");
                // informacion del ticket
                //////CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(db_NTicket.Ticket);

                Mailing.Formato.NuevoTicket FormatoNuevoticket = new Mailing.Formato.NuevoTicket();

                foreach (EmailViewModel email in Emails)
                {
                    CTicketViewModel db_ticketConsulta = await _ticketRepository.GetTicket_Seleccionar_Id(db_ticket.Ticket);
                    var protocol = Request.Scheme;
                    var host = Request.Host.Value;

                    db_ticketConsulta.Direccion = protocol + "://" + host + "";

                    string html = FormatoNuevoticket.HtmlNuevoTicket(email, db_ticketConsulta);

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
                        Asunto = "Nuevo ticket registrado, folio " + db_ticketConsulta.Folio.Folio,                 // Asunto del correo
                                                                                                            // Contenido = "[mailing] <br/> <h1>Correo de prueba</h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                        Contenido = html,
                        fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")       // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                    };

                    Task<Mail> envio = Email.SendAsync(correoNuevo);
                }
            }
            
            return (db_ticket);
        }


        [HttpPost]
        [Route("TicketsAsignados")]
        public async Task<dynamic> TicketsAsignados([FromBody] UsuarioViewModel usuario)
        {
            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_EnOperacion_Usuario(usuario);

            return (db_tickets);
        }


        [HttpPost]
        [Route("TicketsAtendidos")]
        public async Task<dynamic> TicketsAtendidos([FromBody] UsuarioViewModel usuario)
        {
            List<CTicketViewModel> db_tickets = await _ticketRepository.GetTicket_Selecionar_AtendidosOperador(usuario);

            return (db_tickets);
        }

    }
}
