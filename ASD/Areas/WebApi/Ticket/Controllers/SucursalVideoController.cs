using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("VidSucursal")]
        public async Task<dynamic> GetSucursalVideo_Seleccionar_IdTikcet([FromBody] TicketViewModel ticket)
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

            return db_Video;
        }

        ///CON BASE 64
        [HttpPost]
        [Route("UploadVideoSucursal")]
        public async Task<IActionResult> UploadVideoSucursal([FromBody] SucursalVideoViewModel request)
        {
            // Crear los objetos necesarios
            UsuarioViewModel usuario = new UsuarioViewModel { Id = request.Usuario.Id };  // Usar el ID del usuario directamente del request
            SucursalVideoViewModel sucursalVideo = new SucursalVideoViewModel();
            Cat_SucursalVideoViewModel catSucursalVideo = new Cat_SucursalVideoViewModel { Id = request.Cat_SucursalVideo.Id };
            TicketViewModel ticket = new TicketViewModel { Id = request.Ticket.Id };

            // Asignar datos al modelo de SucursalVideo
            sucursalVideo.Ticket = ticket;
            sucursalVideo.Cat_SucursalVideo = catSucursalVideo;

            // Obtener los datos del ticket
            CTicketViewModel dbTicket = await _ticketRepository.GetTicket_Seleccionar_Id(sucursalVideo.Ticket);
            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

            // Generar un nombre único para el archivo de video
            string extension = Path.GetExtension(request.NmOriginal); // Obtener la extensión del archivo
            string nombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + extension;

            // Definir el directorio base
            string directorioBase = $"C:\\File\\ASD\\{dbTicket.Folio.Folio}\\UNIDAD_NEGOCIO\\VIDEO\\";
            if (!Directory.Exists(directorioBase))
            {
                Directory.CreateDirectory(directorioBase);
            }

            // Ruta final del archivo
            string filePath = Path.Combine(directorioBase, nombreArchivo);

            // Convertir base64 a bytes y guardar el archivo de video
            byte[] videoBytes = Convert.FromBase64String(request.NmArchivo);
            await System.IO.File.WriteAllBytesAsync(filePath, videoBytes);

            // Completar los datos del modelo SucursalVideo
            sucursalVideo.Usuario = usuario;
            sucursalVideo.NmOriginal = request.NmOriginal;
            sucursalVideo.NmArchivo = nombreArchivo;

            // Guardar la información del video en la base de datos
            SucursalVideoViewModel dbSucursalVideo = await _sucursalVideoRepository.CreateSucursalVideo(sucursalVideo);

            // Retornar el Id del video creado
            return Ok(new { Id = dbSucursalVideo.Id });
        }


        ///USANDO multipart/form-data
        //[HttpPost]
        //[Route("UploadSucursalVideo")]
        //public async Task<IActionResult> UploadSucursalVideo([FromForm] SucursalVideoViewModel request)
        //{
        //    // Crear los objetos que se van a utilizar
        //    UsuarioViewModel usuario = new UsuarioViewModel { Id = request.Usuario.Id };  // Usar el ID del usuario directamente del request
        //    SucursalVideoViewModel sucursalVideo = new SucursalVideoViewModel();
        //    Cat_SucursalVideoViewModel catSucursalVideo = new Cat_SucursalVideoViewModel { Id = request.Cat_SucursalVideo.Id };
        //    TicketViewModel ticket = new TicketViewModel { Id = request.Ticket.Id };

        //    // Asignar datos al modelo de SucursalVideo
        //    sucursalVideo.Ticket = ticket;
        //    sucursalVideo.Cat_SucursalVideo = catSucursalVideo;

        //    // Obtener los datos del ticket
        //    CTicketViewModel dbTicket = await _ticketRepository.GetTicket_Seleccionar_Id(sucursalVideo.Ticket);
        //    ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

        //    // Generar un nombre único para el archivo usando el control de archivo
        //    string extension = Path.GetExtension(request.NmOriginal); // Obtener la extensión del archivo
        //    string nombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + extension;

        //    // Definir el directorio base
        //    string directorioBase = $"C:\\File\\ASD\\{dbTicket.Folio.Folio}\\UNIDAD_NEGOCIO\\VIDEO\\";
        //    if (!Directory.Exists(directorioBase))
        //    {
        //        Directory.CreateDirectory(directorioBase);
        //    }

        //    // Ruta final del archivo
        //    string filePath = Path.Combine(directorioBase, nombreArchivo);

        //    // Guardar el archivo de video en el directorio
        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await request.File.CopyToAsync(stream);  // Copiar el archivo al directorio
        //    }

        //    // Completar los datos del modelo SucursalVideo
        //    sucursalVideo.Usuario = usuario;
        //    sucursalVideo.NmOriginal = request.NmOriginal;
        //    sucursalVideo.NmArchivo = nombreArchivo;

        //    // Guardar la información del video en la base de datos
        //    SucursalVideoViewModel dbSucursalVideo = await _sucursalVideoRepository.CreateSucursalVideo(sucursalVideo);

        //    // Retornar el Id del video creado
        //    return Ok(new { Id = dbSucursalVideo.Id });
        //}

    }
}