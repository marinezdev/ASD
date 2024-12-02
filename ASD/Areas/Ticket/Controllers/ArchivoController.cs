using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Utilidades;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class ArchivoController : Controller
    {
        private readonly ILogger<ArchivoController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly IControlArchivoRepository _controlArchivoRepository;
        private readonly IArchivoRepository _archivoRepository;

        public ArchivoController(ILogger<ArchivoController> logger, ITicketRepository ticketRepository, IControlArchivoRepository controlArchivoRepository, IArchivoRepository archivoRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _controlArchivoRepository = controlArchivoRepository;
            _archivoRepository = archivoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArchivo(IFormFile file, int IdTicket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = IdTicket;
            ArchivoViewModel archivo = new ArchivoViewModel();
            archivo.Ticket = ticket;

            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

            // Obtiene el nombre completo del archivo
            string fileName = file.FileName;

            // Separa el nombre del archivo de la extensión
            string[] parts = fileName.Split('.');

            // Obtiene la extensión del archivo
            string extension = parts[parts.Length - 1];

            string NombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + "." + extension;

            string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\ARCHIVOS_ADICIONALES\\";

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

                archivo.Usuario = usuario;
                archivo.NmOriginal = file.FileName;
                archivo.NmArchivo = NombreArchivo;
                archivo.Extencion = extension;

                ArchivoViewModel dbArchivo = await _archivoRepository.CreateArchivo(archivo);

                Response.Headers.Add("Content-Length", stream.Length.ToString());
                await stream.CopyToAsync(Response.Body);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<JsonResult> GetArchivo_Seleccionar_IdTicket([FromBody] TicketViewModel ticket)
        {
            List<ArchivoViewModel> db_Archivos = await _archivoRepository.GetArchivo_Seleccionar_IdTicket(ticket);

            return Json(db_Archivos);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteArchivo([FromBody] ArchivoViewModel archivo)
        {
            ArchivoViewModel db_Archivos = await _archivoRepository.DeleteArchivo(archivo);

            return Json(db_Archivos);
        }



        [HttpPost]
        public async Task<IActionResult> AddListArchivo(IFormFile file)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ArchivoViewModel archivo = new ArchivoViewModel();
            List<ArchivoViewModel> sesion_archivos = new List<ArchivoViewModel>();

            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            if (HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket") != null)
            {
                sesion_archivos = HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket");
                //equipo.Id = sesion_equipo.Last().Id + 1;
            }


            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();
            // Obtiene el nombre completo del archivo
            string fileName = file.FileName;

            // Separa el nombre del archivo de la extensión
            string[] parts = fileName.Split('.');

            // Obtiene la extensión del archivo
            string extension = parts[parts.Length - 1];

            string NombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + "." + extension;

            string directorioBase = "C:\\File\\ASD\\ARCHIVOS_TEMPORALES\\";

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

                archivo.Id = controlArchivo.Id;
                archivo.Usuario = usuario;
                archivo.NmOriginal = file.FileName;
                archivo.NmArchivo = NombreArchivo;
                archivo.Extencion = extension;

                // Agregamos el archivo a la sesion
                sesion_archivos.Add(archivo);
                HttpContext.Session.Set<List<ArchivoViewModel>>("ListArchivosTicket", sesion_archivos);

                Response.Headers.Add("Content-Length", stream.Length.ToString());
                await stream.CopyToAsync(Response.Body);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<JsonResult> GetListArchivo()
        {
            List<ArchivoViewModel> sesion_archivos = new List<ArchivoViewModel>();

            if (HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket") != null)
            {
                sesion_archivos = HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket");
            }

            return Json(sesion_archivos);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteListArchivo([FromBody] ArchivoViewModel archivo)
        {
            List<ArchivoViewModel> sesion_equipo = new List<ArchivoViewModel>();

            if (HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket") != null)
            {
                sesion_equipo = HttpContext.Session.Get<List<ArchivoViewModel>>("ListArchivosTicket");
                int index = sesion_equipo.FindIndex(x => x.Id == archivo.Id);
                sesion_equipo.RemoveAt(index);
            }

            if (sesion_equipo.Count == 0)
            {
                HttpContext.Session.Remove("ListArchivosTicket");
            }
            else
            {
                HttpContext.Session.Set<List<ArchivoViewModel>>("ListArchivosTicket", sesion_equipo);
            }

            return Json(sesion_equipo);
        }

    }
}
