using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Route("ImgSucursal")]
        public async Task<dynamic> ImgSucursal([FromBody] TicketViewModel ticket)
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

            return db_Imagen;
        }


        [HttpPost]
        [Route("UploadImgSucursal")]
        public async Task<IActionResult> UploadImgSucursal([FromBody] SucursalImgViewModel request)
        {
            // Crear los objetos que se van a utilizar
            UsuarioViewModel usuario = new UsuarioViewModel { Id = request.Usuario.Id };  // Usar el ID del usuario directamente del request
            SucursalImgViewModel sucursalImg = new SucursalImgViewModel();
            Cat_SucursalImagenViewModel catSucursalImagen = new Cat_SucursalImagenViewModel { Id = request.Cat_SucursalImagen.Id };
            TicketViewModel ticket = new TicketViewModel { Id = request.Ticket.Id };

            // Asignar datos al modelo de SucursalImg
            sucursalImg.Ticket = ticket;
            sucursalImg.Cat_SucursalImagen = catSucursalImagen;

            // Obtener los datos del ticket
            CTicketViewModel dbTicket = await _ticketRepository.GetTicket_Seleccionar_Id(sucursalImg.Ticket);
            ControlArchivoViewModel controlArchivo = await _controlArchivoRepository.CreateControlArchivo();

            // Generar un nombre único para el archivo usando el control de archivo
            string extension = Path.GetExtension(request.NmOriginal); // Obtener la extensión del archivo
            string nombreArchivo = controlArchivo.Clave + controlArchivo.Id.ToString().PadLeft(12, '0') + extension;

            // Definir el directorio base
            string directorioBase = $"C:\\File\\ASD\\{dbTicket.Folio.Folio}\\UNIDAD_NEGOCIO\\";
            if (!Directory.Exists(directorioBase))
            {
                Directory.CreateDirectory(directorioBase);
            }

            // Ruta final del archivo
            string filePath = Path.Combine(directorioBase, nombreArchivo);

            // Convertir base64 a bytes y guardar el archivo
            byte[] imageBytes = Convert.FromBase64String(request.NmArchivo);
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            // Completar los datos del modelo SucursalImg
            sucursalImg.Usuario = usuario;
            sucursalImg.NmOriginal = request.NmOriginal;
            sucursalImg.NmArchivo = nombreArchivo;

            // Guardar la información de la imagen en la base de datos
            SucursalImgViewModel dbSucursalImagen = await _sucursalImgRepository.CreateSucursalImg(sucursalImg);

            // Retornar el Id de la imagen creada
            return Ok(new { Id = dbSucursalImagen.Id });
        }
    }
}