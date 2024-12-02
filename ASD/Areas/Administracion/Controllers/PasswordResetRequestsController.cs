using System;
using System.Threading.Tasks;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Mailing.Views;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Utilidades;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    public class PasswordResetRequestsController : Controller
    {
        private readonly IPasswordResetRequestsRepository _PasswordResetRequestsRepository;

        public PasswordResetRequestsController(IPasswordResetRequestsRepository PasswordResetRequestsRepository)
        {
            _PasswordResetRequestsRepository = PasswordResetRequestsRepository;

        }

        public virtual async Task<IActionResult> RecuperarPass()
        {
            return View();
        }

        [HttpGet()]
        public virtual async Task<IActionResult>ActualizarPass([FromQuery(Name = "cont")] string Token)
        {
            PasswordResetRequestsViewModel _model = new PasswordResetRequestsViewModel();
            _model.Token = Token;
            var protocol = Request.Scheme;
            var host = Request.Host.Value;

            PasswordResetRequestsViewModel R_solicitud = await _PasswordResetRequestsRepository.PasswordResetRequests_Consult(_model);

            if (R_solicitud.Id > 0)
            {
                ViewBag.User = R_solicitud.Usuario.Id;
                return View();

            }
            else
            {
                return View("RequestExp");
            }

        }

        public virtual async Task<IActionResult> RequestExp()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> PasswordResetRequests_Insert([FromBody] PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel R = await _PasswordResetRequestsRepository.PasswordResetRequests_Insert(_model);

            //PRUEBA PARA ENVIAR CORREO
            if (R.Id ==1)
            {
                Mailing.Formato.PasswordRequestResset passwordRequest = new Mailing.Formato.PasswordRequestResset();

                var protocol = Request.Scheme;
                var host = Request.Host.Value;

                R.Direccion = protocol + "://" + host + "";

                string html = passwordRequest.HTMLPasswordRequestResset(R);
                // New EMail    
                MailCreate correoNuevo = new MailCreate()
                {
                    Token = string.Empty,                                                       // dejar vacío | es el identificador que regresará el API para la identificación del Correo
                    AplicacionUserId = 1,                                                       // Id de aplicación. Este se asignara en su momento, para pruebas dejar el valor de 1
                    Aplicacion = "ASD",                                                         // Aplicación. Este se asignara en su momento, para pruebas dejar el valor de "Pruebas"
                    Origen = "ASAE SERVICE DESK (ASD)",                                         // nombre como se desea que aparezca el correo
                    Destinatario = R.Email.Email,                                               // Correo del destinatario
                    DestinatarioCC = string.Empty,                                              // Agregar un destinatario con copia
                    DestinatarioCCO = string.Empty,                                             // Agregar un destinatario con copia oculta
                    Asunto = "Recuperación de contraseña",                                      // Asunto del correo
                                                                                                // Contenido = "[mailing] <br/> <h1> Correo de prueba </h1>",                    // si se quiere que se le de seguimiento al envio de correo se tiene que establecer la leyenda de [mailing], el cuerpo de correo es en formato HTML
                    Contenido = html,
                    fechaEnvio = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")                   // Fecha y hora en que se desea enviar, si se desea enviar de manera inmedita hay que establecer la fecha y hr actual
                };

                Task<Mail> envio = Email.SendAsync(correoNuevo);
            }

            return Json(R);
        }

        [HttpPost]
        public async Task<JsonResult> Usuario_ActualizarPass([FromBody] PasswordResetRequestsViewModel _model)
        {
            _model.Usuario.Password = Encriptar.EncriptarClave(_model.Usuario.Password);
            PasswordResetRequestsViewModel R = await _PasswordResetRequestsRepository.Usuario_ActualizarPass(_model);
           
            return Json(R);
        }
    }
}
