using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using ASD.Areas.WebApi.Dhasbord.Models;
using ASD.Repository.Interface.Dhasbord;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.WebApi.Dhasbord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadorController : Controller
    {
        private readonly IDPrioridadRepository _dprioridadRepository;
        private readonly IDPendientesRepository _dPendientesRepository;

        public OperadorController(IDPrioridadRepository dPrioridadRepository, IDPendientesRepository dPendientesRepository)
        {
            _dprioridadRepository = dPrioridadRepository;
            _dPendientesRepository = dPendientesRepository;
        }

        [HttpPost]
        [Route("Dhasbord")]
        public async Task<dynamic> DhasbordOperador([FromBody] UsuarioViewModel usuario)
        {
            DOperadorModels dbOperadorModels = new DOperadorModels();

            List<DPrioridadViewModel> dbPrioridadTotal = await _dprioridadRepository.GetDhasbord_Prioridad_Operacion(usuario);
            DPendientesViewModel dbPendientes = await _dPendientesRepository.GetDhasbord_Pendientes_IdUsuario(usuario);

            dbOperadorModels.TicketsPrioridad = dbPrioridadTotal;
            dbOperadorModels.TicketsPendientes = dbPendientes;

            return (dbOperadorModels);
        }
    }
}