using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.TicketAtencionMovil
{
    [Route("api/[controller]")]
    [ApiController]

    public class UnidadNegocioController : Controller
    {
        private readonly ILogger<UnidadNegocioController> _logger;
        private readonly ITicketRepository _ticketRepository;
        private readonly ISucursalVideoRepository _sucursalVideoRepository;
        private readonly ISucursalImgRepository _sucursalImgRepository;

        public UnidadNegocioController(ILogger<UnidadNegocioController> logger, ITicketRepository ticketRepository, ISucursalVideoRepository sucursalVideoRepository, ISucursalImgRepository sucursalImgRepository)
        {
            _logger = logger;
            _ticketRepository = ticketRepository;
            _sucursalVideoRepository = sucursalVideoRepository;
            _sucursalImgRepository = sucursalImgRepository;
        }


        [HttpPost]
        [Route("InfoSocursal")]
        public async Task<dynamic> InfoSocursal([FromBody] TicketViewModel ticket)
        {

            CTicketViewModel db_ticket = await _ticketRepository.GetTicket_Seleccionar_Id(ticket);

            return db_ticket;
        }

    }
}