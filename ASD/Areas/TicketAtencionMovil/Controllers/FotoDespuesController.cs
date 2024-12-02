using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static ASD.Repository.Interface.Administracion.IUrlRepository;
using static OpenQA.Selenium.VirtualAuth.VirtualAuthenticatorOptions;

namespace ASD.Areas.TicketAtencionMovil.Controllers
{
    [Authorize]
    [Area("TicketAtencionMovil")]
    public class FotoDespuesController : Controller
    {
        private readonly ILogger<FotoDespuesController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketEquipoRepository _ticketEquipoRepository;
        private readonly ITicketEquipoImagenRepository _ticketEquipoImagenRepository;

        public FotoDespuesController(ILogger<FotoDespuesController> logger, ITicketRepository ticketRepository, ITicketEquipoRepository ticketEquipoRepository, ITicketEquipoImagenRepository ticketEquipoImagenRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _ticketEquipoRepository = ticketEquipoRepository;
            _ticketEquipoImagenRepository = ticketEquipoImagenRepository;
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Index([FromQuery(Name = "cont")] string Id)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(Id));

            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);
            //List<CTicketEquipoViewModel> dbEquipos = await _ticketEquipoRepository.GetTicketEquipo_Seleccionar_IdTicket(ticket);
            //List<CTicketEquipoViewModel> dbRutinaImg = new List<CTicketEquipoViewModel>();

            //// Obtiene el protocolo (http o https)
            //var protocol = Request.Scheme;

            //// Obtiene el host (www.ejemplo.com)
            //var host = Request.Host.Value;

            //foreach (CTicketEquipoViewModel cTicketEquipo in dbEquipos)
            //{
            //    TicketEquipoViewModel ticketEquipo = new TicketEquipoViewModel();
            //    ticketEquipo.Id = cTicketEquipo.Id;
            //    cTicketEquipo.ticketEquipoImagen = await _ticketEquipoImagenRepository.GetTicketEquipoImagen_Seleccionar_IdTicketEquipo(ticketEquipo);

            //    if (cTicketEquipo.ticketEquipoImagen != null)
            //    {
            //        for (int i = 0; i < cTicketEquipo.ticketEquipoImagen.Count; i++)
            //        {
            //            cTicketEquipo.ticketEquipoImagen[i].Imagen = protocol + "://" + host + "/Ticket/TicketEquipoImagen/ImageView?cont=" + cTicketEquipo.ticketEquipoImagen[i].Id;
            //            cTicketEquipo.ticketEquipoImagen[i].ImagenVS = protocol + "://" + host + "/Ticket/TicketEquipoImagen/ImageVSView?cont=" + cTicketEquipo.ticketEquipoImagen[i].Id;
            //        }
            //    }

            //    dbRutinaImg.Add(cTicketEquipo);
            //}

            ViewBag.Ticket = db_ticket;
            //ViewBag.RutinaImg = dbRutinaImg;
            return View();
        }
    }
}
