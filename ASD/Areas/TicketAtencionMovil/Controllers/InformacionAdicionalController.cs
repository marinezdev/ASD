using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Repository.Interface.Administracion;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Interface.TicketAtencionMovil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.TicketAtencionMovil.Controllers
{
    [Authorize]
    [Area("TicketAtencionMovil")]
    public class InformacionAdicionalController : Controller
    {
        private readonly ILogger<InformacionAdicionalController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISucursalImgRepository _sucursalImgRepository;
        private readonly IArchivoRepository _ArchivoRepository;
        private readonly ISucursalVideoRepository _sucursalVideoRepository;
        private readonly IFirmaElectronicaRepository _firmaElectronicaRepository;


        public InformacionAdicionalController(ILogger<InformacionAdicionalController> logger, ITicketRepository ticketRepository, ISucursalImgRepository sucursalImgRepository, IArchivoRepository archivoRepository, ISucursalVideoRepository sucursalVideoRepository, IFirmaElectronicaRepository firmaElectronicaRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _sucursalImgRepository = sucursalImgRepository;
            _ArchivoRepository = archivoRepository;
            _sucursalVideoRepository = sucursalVideoRepository;
            _firmaElectronicaRepository = firmaElectronicaRepository;

        }

        [HttpGet()]
        public virtual async Task<IActionResult> Index([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);


            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            //List<SucursalImgViewModel> db_Imagen = await _sucursalImgRepository.GetSucursalImg_Seleccionar_IdTikcet(ticket);
            //List<SucursalVideoViewModel> db_Video = await _sucursalVideoRepository.GetSucursalVideo_Seleccionar_IdTikcet(ticket);
            List<ArchivoViewModel> db_Archivos = await _ArchivoRepository.GetArchivo_Seleccionar_IdTicket(ticket);

            InformacionAdicionalViewModel R = await _firmaElectronicaRepository.GetSignature(ticket);

            //// Obtiene el protocolo (http o https)
            //var protocol = Request.Scheme;

            //// Obtiene el host (www.ejemplo.com)
            //var host = Request.Host.Value;

            //if (db_Imagen != null)
            //{
            //    for (int i = 0; i < db_Imagen.Count; i++)
            //    {
            //        db_Imagen[i].NmArchivo = protocol + "://" + host + "/Ticket/SucursalImg/ImageView?cont=" + db_Imagen[i].Id;
            //    }
            //}

            //if (db_Video != null)
            //{
            //    for (int i = 0; i < db_Video.Count; i++)
            //    {
            //        db_Video[i].NmArchivo = protocol + "://" + host + "/Ticket/SucursalVideo/VideoView?cont=" + db_Video[i].Id;
            //    }

            //}

            ViewBag.Ticket = db_ticket;
            //ViewBag.Imagen = db_Imagen;
            ViewBag.Archivos = db_Archivos;
            //ViewBag.Video = db_Video;

            ViewBag.Info = R;

            return View();
        }

       

    }
}
