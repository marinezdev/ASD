using ASD.Areas.Empresa.Controllers;
using ASD.Areas.Empresa.Models;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketAsignacionEmpresaController : Controller
    {
        private readonly ILogger<TicketAsignacionEmpresaController> _logger;
        private readonly ITicketAsignacionEmpresaRepository _ticketAsignacionEmpresaRepository;
        public TicketAsignacionEmpresaController(ILogger<TicketAsignacionEmpresaController> logger, ITicketAsignacionEmpresaRepository ticketAsignacionEmpresaRepository)
        {
            _logger = logger;
            _ticketAsignacionEmpresaRepository = ticketAsignacionEmpresaRepository;
        }

        
        [HttpPost]
        public async Task<JsonResult> CreateTicketAsignacionEmpresa([FromBody] TicketAsignacionEmpresaViewModel ticketAsignacionEmpresa)
        {
            TicketAsignacionEmpresaViewModel db_Cuadrillas = await _ticketAsignacionEmpresaRepository.CreateTicketAsignacionEmpresa(ticketAsignacionEmpresa);
            return Json(db_Cuadrillas);
        }
    }
}
