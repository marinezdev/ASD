using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace ASD.Areas.Ticket.Controllers
{
    [Area("Ticket")]
    public class SucursalImgController : Controller
    {
        private readonly ILogger<SucursalImgController> _logger;
        private readonly ISucursalImgRepository _sucursalImgRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IControlArchivoRepository _controlArchivoRepository;

        public SucursalImgController(ILogger<SucursalImgController> logger, ISucursalImgRepository sucursalImgRepository, ITicketRepository ticketRepository, IControlArchivoRepository controlArchivoRepository)
        {
            _logger = logger;
            _sucursalImgRepository = sucursalImgRepository;
            _ticketRepository = ticketRepository;
            _controlArchivoRepository = controlArchivoRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetSucursalImg_Seleccionar_IdTicket([FromBody] TicketViewModel ticket)
        {
            // Obtiene el protocolo (http o https)
            var protocol = Request.Scheme;

            // Obtiene el host (www.ejemplo.com)
            var host = Request.Host.Value;
            List<SucursalImgViewModel> db_Imagen = await _sucursalImgRepository.GetSucursalImg_Seleccionar_IdTikcet(ticket);

            if (db_Imagen != null)
            {
                for (int i = 0; i < db_Imagen.Count; i++)
                {
                    db_Imagen[i].NmArchivo = protocol + "://" + host + "/Ticket/SucursalImg/ImageView?cont=" + db_Imagen[i].Id;
                }
            }

            return Json(db_Imagen);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSucursalImg(IFormFile file,int IdTicket, int Idtipo)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            SucursalImgViewModel SucursalImg = new SucursalImgViewModel();
            Cat_SucursalImagenViewModel cat_SucursalImagen = new Cat_SucursalImagenViewModel();
            cat_SucursalImagen.Id = Idtipo;
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = IdTicket;

            SucursalImg.Ticket = ticket;
            SucursalImg.Cat_SucursalImagen = cat_SucursalImagen;

            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;
            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(SucursalImg.Ticket);
            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

            // Obtiene el nombre completo del archivo
            string fileName = file.FileName;

            // Separa el nombre del archivo de la extensión
            string[] parts = fileName.Split('.');

            // Obtiene la extensión del archivo
            string extension = parts[parts.Length - 1];

            string NombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + "." + extension;

            string directorioBase = "C:\\File\\ASD\\" + db_ticket.Folio.Folio + "\\UNIDAD_NEGOCIO\\";

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

                SucursalImg.Usuario = usuario;
                SucursalImg.NmOriginal = file.FileName;
                SucursalImg.NmArchivo = NombreArchivo;

                SucursalImgViewModel dbSucursalImagen = await _sucursalImgRepository.CreateSucursalImg(SucursalImg);

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
            SucursalImgViewModel SucursalImg = new SucursalImgViewModel();
            SucursalImg.Id = Convert.ToInt32(Id);

            SucursalImgViewModel dbSucursalImg = await _sucursalImgRepository.GetSucursalImg_Seleccionar_Id(SucursalImg);
            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(dbSucursalImg.Ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string imageDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "UNIDAD_NEGOCIO"); // Assuming images are stored in "IMAGENES"


            if (dbSucursalImg != null && dbSucursalImg.Id > 0)
            {
                string filePath = Path.Combine(imageDirectory, dbSucursalImg.NmArchivo);

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


                Response.Headers.Add("Content-Disposition", $"inline; filename={dbSucursalImg.NmArchivo}");

                return File(fileStream, contentType);
            }


            return NotFound();
        }
    }
}
