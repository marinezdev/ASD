using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using ASD.Areas.Administracion.Models;
using System.Security.Claims;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketEquipoImagenController : Controller
    {
        private readonly ILogger<TicketEquipoImagenController> _logger;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;
        private readonly ITicketEquipoImagenRepository _ticketEquipoImagenRepository;
        private readonly ITicketEquipoRutinaRepository _ticketEquipoRutinaRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IControlArchivoRepository _controlArchivoRepository;
        public TicketEquipoImagenController(ILogger<TicketEquipoImagenController> logger, ITicketEquipoImagenRepository ticketEquipoImagenRepository, ITicketRepository ticketRepository, IControlArchivoRepository controlArchivoRepository, ITicketEquipoRepository ticketEquipoRepository, ITicketEquipoRutinaRepository ticketEquipoRutinaRepository)
        {
            _logger = logger;
            _ticketEquipoImagenRepository = ticketEquipoImagenRepository;
            _ticketRepository = ticketRepository;
            _controlArchivoRepository = controlArchivoRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
            _ticketEquipoRutinaRepository = ticketEquipoRutinaRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetTicketEquipoImagen_Seleccionar_IdTicketEquipo([FromBody] TicketViewModel ticket)
        {
            // Obtiene el protocolo (http o https)
            var protocol = Request.Scheme;

            // Obtiene el host (www.ejemplo.com)
            var host = Request.Host.Value;

            List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);
            List<CTicketEquipoViewModel> dbRutinaImg = new List<CTicketEquipoViewModel>();

            if(dbEquipos != null)
            {
                foreach (CTicketEquipoViewModel cTicketEquipo in dbEquipos)
                {
                    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
                    ticketEquipo.Id = cTicketEquipo.Id;
                    cTicketEquipo.ticketEquipoRutina = await _ticketEquipoRutinaRepository.GetTicketEquipoRutina_Seleccionar_IdTicketEquipo(ticketEquipo);
                    cTicketEquipo.ticketEquipoImagen = await _ticketEquipoImagenRepository.GetTicketEquipoImagen_Seleccionar_IdTicketEquipo(ticketEquipo);

                    if (cTicketEquipo.ticketEquipoImagen != null)
                    {
                        for (int i = 0; i < cTicketEquipo.ticketEquipoImagen.Count; i++)
                        {
                            cTicketEquipo.ticketEquipoImagen[i].Imagen = protocol + "://" + host + "/Ticket/TicketEquipoImagen/ImageView?cont=" + cTicketEquipo.ticketEquipoImagen[i].Id;
                            cTicketEquipo.ticketEquipoImagen[i].ImagenVS = protocol + "://" + host + "/Ticket/TicketEquipoImagen/ImageVSView?cont=" + cTicketEquipo.ticketEquipoImagen[i].Id;
                        }
                    }
                    dbRutinaImg.Add(cTicketEquipo);
                }
            }
            
            return Json(dbRutinaImg);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateTicketEquipoImagen_Actualizar(IFormFile file, int IdTicket, int IdTicketEquipoImagen)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            TicketEquipoImagenViewModel TicketEquipoImagen = new TicketEquipoImagenViewModel();

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = IdTicket;

            TicketEquipoImagen.Id = IdTicketEquipoImagen;

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

            string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\IMG_ANTES\\";

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

                TicketEquipoImagen.Imagen = NombreArchivo;

                TicketEquipoImagenViewModel dbSucursalImagen = await _ticketEquipoImagenRepository.UpdateTicketEquipoImagen_Actualizar(TicketEquipoImagen);

                Response.Headers.Add("Content-Length", stream.Length.ToString());
                //Response.Headers.Add("Name", file.FileName);
                Response.Headers.Add("Id", dbSucursalImagen.Id.ToString());
                await stream.CopyToAsync(Response.Body);

            }

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTicketEquipoImagen_Actualizar_ImagenVS(IFormFile file, int IdTicket, int IdTicketEquipoImagen)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            TicketEquipoImagenViewModel TicketEquipoImagen = new TicketEquipoImagenViewModel();

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = IdTicket;

            TicketEquipoImagen.Id = IdTicketEquipoImagen;

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

            string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\IMG_DESPUES\\";

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

                TicketEquipoImagen.Imagen = NombreArchivo;

                TicketEquipoImagenViewModel dbSucursalImagen = await _ticketEquipoImagenRepository.UpdateTicketEquipoImagen_Actualizar_ImagenVS(TicketEquipoImagen);

                Response.Headers.Add("Content-Length", stream.Length.ToString());
                //Response.Headers.Add("Name", file.FileName);
                Response.Headers.Add("Id", dbSucursalImagen.Id.ToString());
                await stream.CopyToAsync(Response.Body);

            }

            return Ok();
        }
        [HttpGet()]
        public virtual async Task<IActionResult> ImageView([FromQuery(Name = "cont")] string Id)
        {
            TicketEquipoImagenViewModel TicketEquipoImagen = new TicketEquipoImagenViewModel();
            TicketEquipoImagen.Id = Convert.ToInt32(Id);

            TicketEquipoImagenViewModel dbTicketEquipoImagen = await _ticketEquipoImagenRepository.GetTicketEquipoImagen_Seleccionar_Id(TicketEquipoImagen);
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(dbTicketEquipoImagen.TicketEquipo.Ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string imageDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "IMG_ANTES"); // Assuming images are stored in "IMAGENES"


            if (dbTicketEquipoImagen != null && dbTicketEquipoImagen.Id > 0)
            {
                if (dbTicketEquipoImagen.Imagen != null)
                {
                    string filePath = Path.Combine(imageDirectory, dbTicketEquipoImagen.Imagen);

                    // Check if the file exists
                    if (!System.IO.File.Exists(filePath))
                    {
                        return NotFound("El archivo de imagen no existe.");
                    }
                    // Security check: Ensure file path is within expected directory
                    string fullPath = Path.GetFullPath(filePath);
                    if (!fullPath.StartsWith(imageDirectory))
                    {
                        _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                        return NotFound();
                    }

                    var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    // Determine image MIME type based on file extension
                    //string contentType = MimeMapping.GetMimeMapping(filePath);
                    var provider = new FileExtensionContentTypeProvider();
                    string contentType;
                    if (!provider.TryGetContentType(filePath, out contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    Response.Headers.Add("Content-Disposition", $"inline; filename={dbTicketEquipoImagen.Imagen}");

                    return File(fileStream, contentType);
                }
            }


            return NotFound();
        }
        [HttpGet()]
        public virtual async Task<IActionResult> ImageVSView([FromQuery(Name = "cont")] string Id)
        {
            TicketEquipoImagenViewModel TicketEquipoImagen = new TicketEquipoImagenViewModel();
            TicketEquipoImagen.Id = Convert.ToInt32(Id);

            TicketEquipoImagenViewModel dbTicketEquipoImagen = await _ticketEquipoImagenRepository.GetTicketEquipoImagen_Seleccionar_Id(TicketEquipoImagen);
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(dbTicketEquipoImagen.TicketEquipo.Ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string imageDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "IMG_DESPUES"); // Assuming images are stored in "IMAGENES"


            if (dbTicketEquipoImagen != null && dbTicketEquipoImagen.Id > 0)
            {
                string filePath = Path.Combine(imageDirectory, dbTicketEquipoImagen.ImagenVS);

                // Security check: Ensure file path is within expected directory
                string fullPath = Path.GetFullPath(filePath);
                if (!fullPath.StartsWith(imageDirectory))
                {
                    _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Determine image MIME type based on file extension
                //string contentType = MimeMapping.GetMimeMapping(filePath);
                var provider = new FileExtensionContentTypeProvider();
                string contentType;
                if (!provider.TryGetContentType(filePath, out contentType))
                {
                    contentType = "application/octet-stream";
                }

                Response.Headers.Add("Content-Disposition", $"inline; filename={dbTicketEquipoImagen.ImagenVS}");

                return File(fileStream, contentType);
            }


            return NotFound();
        }
    }
}
