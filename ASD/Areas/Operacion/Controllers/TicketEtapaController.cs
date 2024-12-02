using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ASD.Repository.Interface.Dhasbord;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Operacion.Models.Consulta;
using ASD.Repository.Interface.Operacion;
using ASD.Areas.Ticket.Controllers;

namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class TicketEtapaController : Controller
    {
        private readonly ILogger<TicketEtapaController> _logger;
        private readonly ITicketEtapaRepository _ticketEtapaRepository;

        public TicketEtapaController(ILogger<TicketEtapaController> logger, ITicketEtapaRepository ticketEtapaRepository)
        {
            _logger = logger;
            _ticketEtapaRepository = ticketEtapaRepository;
        }

        [HttpPost]
        public async Task<JsonResult> GetTicketEtapa_Consualta_IdTicket([FromBody] TicketViewModel ticket)
        {
            List<CTicketEtapa2ViewModel> db_Etapas = await _ticketEtapaRepository.GetTicketEtapa_Consualta_IdTicket(ticket);
            return Json(db_Etapas);
        }

    }
}
