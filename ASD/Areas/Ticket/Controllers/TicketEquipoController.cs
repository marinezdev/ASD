using ASD.Areas.Inventario.Controllers;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketEquipoController : Controller
    {
        private readonly ILogger<TicketEquipoController> _logger;
        public TicketEquipoController(ILogger<TicketEquipoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<JsonResult> GetListTicketEquipo()
        {
            List<EquipoViewModel> sesion_equipo = new List<EquipoViewModel>();

            if (HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket") != null)
            {
                sesion_equipo = HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket");
            }
            return Json(sesion_equipo);
        }
        [HttpPost]
        public async Task<JsonResult> AddListTicketEquipo([FromBody] EquipoViewModel equipo)
        {
            List<EquipoViewModel> sesion_equipo = new List<EquipoViewModel>();

            if (HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket") != null)
            {
                sesion_equipo = HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket");
                //equipo.Id = sesion_equipo.Last().Id + 1;
            }
            //else
            //{
            //    //equipo.Id = 1;
            //}

            sesion_equipo.Add(equipo);
            HttpContext.Session.Set<List<EquipoViewModel>>("ListEquipoTicket", sesion_equipo);

            return Json(sesion_equipo);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteListTicketEquipo([FromBody] EquipoViewModel equipo)
        {
            List<EquipoViewModel> sesion_equipo = new List<EquipoViewModel>();

            if (HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket") != null)
            {
                sesion_equipo = HttpContext.Session.Get<List<EquipoViewModel>>("ListEquipoTicket");
                int index = sesion_equipo.FindIndex(x => x.Id == equipo.Id);
                sesion_equipo.RemoveAt(index);
            }

            if (sesion_equipo.Count == 0)
            {
                HttpContext.Session.Remove("ListEquipoTicket");
            }
            else
            {
                HttpContext.Session.Set<List<EquipoViewModel>>("ListEquipoTicket", sesion_equipo);
            }

            return Json(sesion_equipo);
        }

    }
}
