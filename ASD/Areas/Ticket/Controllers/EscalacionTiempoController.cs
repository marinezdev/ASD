using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class EscalacionTiempoController : Controller
    {
        private readonly ILogger<EscalacionTiempoController> _logger;
        private readonly IEscalacionTiempoRepository _ecscalacionTiempoRepository;

        public EscalacionTiempoController(ILogger<EscalacionTiempoController> logger, IEscalacionTiempoRepository ecscalacionTiempoRepository)
        {
            _logger = logger;
            _ecscalacionTiempoRepository = ecscalacionTiempoRepository;
        }

        [HttpPost]
        public async Task<JsonResult> CreateEscalacionTiempo([FromBody] EscalacionTiempoViewModel escalacionTiempo)
        {
            EscalacionTiempoViewModel db_EscalacionTiempo = await _ecscalacionTiempoRepository.CreateEscalacionTiempo_Insertar(escalacionTiempo);

            return Json(db_EscalacionTiempo);
        }

        [HttpPost]
        public async Task<JsonResult> GetEscalacionTiempo_Seleccionar_IdFlujo([FromBody] EscalacionTiempoViewModel escalacionTiempo)
        {
            List<EscalacionTiempoViewModel> db_EscalacionTiempo = await _ecscalacionTiempoRepository.GetEscalacionTiempo_Seleccionar_IdFlujo(escalacionTiempo);
            return Json(db_EscalacionTiempo);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteEscalacionTiempo([FromBody] EscalacionTiempoViewModel escalacionTiempo)
        {
            EscalacionTiempoViewModel db_EscalacionTiempo = await _ecscalacionTiempoRepository.DeleteEscalacionTiempo_Eliminar(escalacionTiempo);
            return Json(db_EscalacionTiempo);
        }
    }
}
