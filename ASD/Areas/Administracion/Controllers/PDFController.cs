using ASD.Areas.Ticket.Controllers;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Administracion;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;

namespace ASD.Areas.Administracion.Controllers
{
    [Area("Administracion")]

    public class PDFController : Controller
    {
        private readonly ILogger<PDFController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly IOrdenTrabajoRepository _ordenTrabajoRepository;

        public PDFController(ILogger<PDFController> logger, IControlArchivoRepository controlArchivoRepository, ITicketRepository ticketRepository, IOrdenTrabajoRepository ordenTrabajoRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _ordenTrabajoRepository = ordenTrabajoRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> PDFView([FromQuery(Name = "cont")] string Id)
        {
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(Id);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string pdfDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "ORDENTRABAJO");

            OrdenTrabajoViewModel ordenTrabajo = await _ordenTrabajoRepository.GetOrdenTrabajo_Seleccionar_IdTicket(ticket);

            if (ordenTrabajo != null && ordenTrabajo.Id > 0)
            {
                string filePath = Path.Combine(pdfDirectory, ordenTrabajo.NmArchivo);

                string fullPath = Path.GetFullPath(filePath);
                if (!fullPath.StartsWith(pdfDirectory))
                {
                    _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                Response.Headers.Add("Content-Disposition", $"inline; filename={ordenTrabajo.NmArchivo}");

                return File(fileStream, "application/pdf");
            }

            return NotFound();
        }

        [HttpGet()]
        public async Task<IActionResult> PDFViewArchivoAdicional([FromQuery(Name = "cont")] string IdTicket, [FromQuery(Name = "archivo")] string IdArchivo)
        {
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdTicket);

            ArchivoViewModel archivo = new ArchivoViewModel();
            archivo.Id = Convert.ToInt32(IdArchivo);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string pdfDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "ARCHIVOS_ADICIONALES");

            OrdenTrabajoViewModel ordenTrabajo = await _ordenTrabajoRepository.Archivo_Seleccionar_Id(archivo);

            if (ordenTrabajo != null && ordenTrabajo.Id > 0)
            {
                string filePath = Path.Combine(pdfDirectory, ordenTrabajo.NmArchivo);

                string fullPath = Path.GetFullPath(filePath);
                if (!fullPath.StartsWith(pdfDirectory))
                {
                    _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                Response.Headers.Add("Content-Disposition", $"inline; filename={ordenTrabajo.NmArchivo}");

                return File(fileStream, "application/pdf");
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ImagenViewArchivoAdicional([FromQuery(Name = "cont")] string IdTicket, [FromQuery(Name = "archivo")] string IdArchivo)
        {
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdTicket);

            ArchivoViewModel archivo = new ArchivoViewModel();
            archivo.Id = Convert.ToInt32(IdArchivo);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string imageDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "ARCHIVOS_ADICIONALES");

            OrdenTrabajoViewModel ordenTrabajo = await _ordenTrabajoRepository.Archivo_Seleccionar_Id(archivo);

            if (ordenTrabajo != null && ordenTrabajo.Id > 0)
            {
                string filePath = Path.Combine(imageDirectory, ordenTrabajo.NmArchivo);

                string fullPath = Path.GetFullPath(filePath);
                if (!fullPath.StartsWith(imageDirectory))
                {
                    _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                string mimeType;
                switch (Path.GetExtension(filePath).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        mimeType = "image/jpeg";
                        break;
                    case ".png":
                        mimeType = "image/png";
                        break;
                    case ".gif":
                        mimeType = "image/gif";
                        break;
                    default:
                        mimeType = "application/octet-stream";
                        break;
                }

                return File(fileStream, mimeType);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DescargaViewArchivoAdicional([FromQuery(Name = "cont")] string IdTicket, [FromQuery(Name = "archivo")] string IdArchivo)
        {
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdTicket);

            ArchivoViewModel archivo = new ArchivoViewModel();
            archivo.Id = Convert.ToInt32(IdArchivo);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            if (db_ticket == null)
            {
                return NotFound("No se encontró el ticket correspondiente.");
            }

            string fileDirectory = Path.Combine("C:", "File", "ASD", db_ticket.Folio.Folio, "ARCHIVOS_ADICIONALES");

            OrdenTrabajoViewModel ordenTrabajo = await _ordenTrabajoRepository.Archivo_Seleccionar_Id(archivo);

            if (ordenTrabajo != null && ordenTrabajo.Id > 0)
            {
                string filePath = Path.Combine(fileDirectory, ordenTrabajo.NmArchivo);

                string fullPath = Path.GetFullPath(filePath);
                if (!fullPath.StartsWith(fileDirectory))
                {
                    _logger.LogWarning("Intento de acceso a un archivo fuera de la ruta esperada.");
                    return NotFound();
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                string mimeType;
                switch (Path.GetExtension(filePath).ToLower())
                {
                    case ".doc":
                        mimeType = "application/msword";
                        break;
                    case ".docx":
                        mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".xls":
                        mimeType = "application/vnd.ms-excel";
                        break;
                    case ".xlsx":
                        mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                    case ".rar":
                        mimeType = "application/x-rar-compressed";
                        break;
                    case ".zip":
                        mimeType = "application/zip";
                        break;
                    default:
                        mimeType = "application/octet-stream";
                        break;
                }

                Response.Headers.Add("Content-Disposition", $"attachment; filename={ordenTrabajo.NmArchivo}");

                return File(fileStream, mimeType);
            }

            return NotFound();
        }

    }
}
