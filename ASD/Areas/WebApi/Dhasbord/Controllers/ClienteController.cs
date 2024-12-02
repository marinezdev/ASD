using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.WebApi.Dhasbord.Models;
using ASD.Repository.Interface.Dhasbord;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASD.Areas.WebApi.Dhasbord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IDTicketRepository _dticketRepositoy;
        private readonly IDEstatusTicketRepository _DEstatusTicketRepository;
        private readonly IDPrioridadRepository _DprioridadRepository;

        public ClienteController(ILogger<ClienteController> logger, IDTicketRepository dticketRepository, IDEstatusTicketRepository dEstatusTicketRepository, IDPrioridadRepository dprioridadRepository)
        {
            _logger = logger;
            _dticketRepositoy = dticketRepository;
            _DEstatusTicketRepository = dEstatusTicketRepository;
            _DprioridadRepository = dprioridadRepository;
        }

        [HttpPost]
        [Route("Dhasbord")]
        public async Task<dynamic> DhasbordClienteAsync([FromBody] UsuarioViewModel usuario)
        {
            DClienteModels dbClienteModels = new DClienteModels();

            List<DTicketViewModel> dTicketsFlujo = await _dticketRepositoy.GetCliente_Ticket_Flujo(usuario);
            List<DTicketViewModel> dbTicketsEstatus = await _DEstatusTicketRepository.GetCliente_Ticket_EstatusTicket(usuario);
            List<DPrioridadViewModel> dbTicketsPrioridad = await _DprioridadRepository.GetCliente_Ticket_Prioridad(usuario);

            dbClienteModels.TicketsFlujos = dTicketsFlujo;
            dbClienteModels.TicketsEstatus = dbTicketsEstatus;
            dbClienteModels.TicketsPrioridad = dbTicketsPrioridad;

            return (dbClienteModels);
        }
    }
}
