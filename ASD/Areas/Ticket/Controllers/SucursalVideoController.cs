using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.StaticFiles;

namespace ASD.Areas.Ticket.Controllers
{
    [Area("Ticket")]
    public class SucursalVideoController : Controller
    {
        private readonly ILogger<SucursalVideoController> _logger;
        private readonly ISucursalVideoRepository _sucursalVideoRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IControlArchivoRepository _controlArchivoRepository;

        public SucursalVideoController(ILogger<SucursalVideoController> logger, ISucursalVideoRepository sucursalVideoRepository, ITicketRepository ticketRepository, IControlArchivoRepository controlArchivoRepository)
        {
            _logger = logger;
            _sucursalVideoRepository = sucursalVideoRepository;
            _ticketRepository = ticketRepository;
            _controlArchivoRepository = controlArchivoRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetSucursalVideo_Seleccionar_IdTikcet([FromBody] TicketViewModel ticket)
        {
            // Obtiene el protocolo (http o https)
            var protocol = Request.Scheme;

            // Obtiene el host (www.ejemplo.com)
            var host = Request.Host.Value;
            List<SucursalVideoViewModel> db_Video = await _sucursalVideoRepository.GetSucursalVideo_Seleccionar_IdTikcet(ticket);

            if (db_Video != null)
            {
                for (int i = 0; i < db_Video.Count; i++)
                {
                    db_Video[i].NmArchivo = protocol + "://" + host + "/Ticket/SucursalVideo/VideoView?cont=" + db_Video[i].Id;
                }
            }

            return Json(db_Video);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSucursalVideo(IFormFile file, int IdTicket, int Idtipo)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            SucursalVideoViewModel SucursalVideo= new SucursalVideoViewModel();
            Cat_SucursalVideoViewModel cat_SucursalVideo = new Cat_SucursalVideoViewModel();
            cat_SucursalVideo.Id = Idtipo;
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = IdTicket;

            SucursalVideo.Ticket = ticket;
            SucursalVideo.Cat_SucursalVideo = cat_SucursalVideo;

            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;
            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(SucursalVideo.Ticket);
            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

            // Obtiene el nombre completo del archivo
            string fileName = file.FileName;

            // Separa el nombre del archivo de la extensión
            string[] parts = fileName.Split('.');

            // Obtiene la extensión del archivo
            string extension = parts[parts.Length - 1];

            string NombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + "." + extension;

            string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\UNIDAD_NEGOCIO\\VIDEO\\";

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

                SucursalVideo.Usuario = usuario;
                SucursalVideo.NmOriginal = file.FileName;
                SucursalVideo.NmArchivo = NombreArchivo;

                SucursalVideoViewModel dbSucursalVideo = await _sucursalVideoRepository.CreateSucursalVideo(SucursalVideo);

                Response.Headers.Add("Content-Length", stream.Length.ToString());
                //Response.Headers.Add("Name", file.FileName);
                Response.Headers.Add("Id", dbSucursalVideo.Id.ToString());
                await stream.CopyToAsync(Response.Body);
            }

            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> VideoView([FromQuery(Name = "cont")] string Id)
        {
            SucursalVideoViewModel SucursalVideo = new SucursalVideoViewModel();
            SucursalVideo.Id = Convert.ToInt32(Id);

            SucursalVideoViewModel dbSucursalVideo = await _sucursalVideoRepository.GetSucursalVideo_Seleccionar_Id(SucursalVideo);
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(dbSucursalVideo.Ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string imageDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "UNIDAD_NEGOCIO", "VIDEO"); // Assuming images are stored in "IMAGENES"


            if (dbSucursalVideo != null && dbSucursalVideo.Id > 0)
            {
                string filePath = Path.Combine(imageDirectory, dbSucursalVideo.NmArchivo);

                // Security check: Ensure file path is within expected directory
                string fullPath = Path.GetFullPath(filePath);
                if (!fullPath.StartsWith(imageDirectory))
                {
                    _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Obtener el tipo MIME basado en la extensión del archivo
                string contentType = GetContentType(dbSucursalVideo.NmArchivo);

                Response.Headers.Add("Content-Disposition", $"inline; filename={dbSucursalVideo.NmArchivo}");

                return File(fileStream, contentType);

                // Determine image MIME type based on file extension
                //string contentType = MimeMapping.GetMimeMapping(filePath);
                //var provider = new FileExtensionContentTypeProvider();
                //string contentType;
                //if (!provider.TryGetContentType(filePath, out contentType))
                //{
                //    contentType = "application/octet-stream";
                //}

                //Response.Headers.Add("Content-Disposition", $"inline; filename={dbSucursalVideo.NmArchivo}");

                //return File(fileStream, contentType);
            }

            return NotFound();
        }

        private string GetContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            switch (extension)
            {
                case ".mov":
                    return "video/quicktime";
                case ".mp4":
                    return "video/mp4";
                case ".wmv":
                    return "video/x-ms-wmv";
                case ".avi":
                    return "video/x-msvideo";
                default:
                    return "application/octet-stream"; // Tipo MIME predeterminado si no se reconoce la extensión
            }
        }

    }
}
