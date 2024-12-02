using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Ticket.Models.Consulta;
using System.Security.Claims;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Ticket.Models;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class TicketFechaAtencionController : Controller
    {
        private readonly ILogger<TicketFechaAtencionController> _logger;
        private readonly ITicketFechaAtencionRepository _ticketFechaAtencionRepository;
        private readonly ICat_PrioridadRepository _cat_PrioridadRepository;

        public TicketFechaAtencionController(ILogger<TicketFechaAtencionController> logger, ITicketFechaAtencionRepository ticketFechaAtencionRepository, ICat_PrioridadRepository cat_PrioridadRepository)
        {
            _logger = logger;
            _ticketFechaAtencionRepository = ticketFechaAtencionRepository;
            _cat_PrioridadRepository = cat_PrioridadRepository;

        }

        public virtual async Task<IActionResult> Index()
        {
            CTicketFechaAtencionViewModel usuario = new CTicketFechaAtencionViewModel();
            string Id;
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }
            //FECHAS MEJORA EN ENCRIPTADO
            List<CTicketFechaAtencionViewModel> R = await _ticketFechaAtencionRepository.Ticket_FechaAtencion_Seleccionar_IdUser(usuario);
            ViewBag.Fechas = R;

            List<Cat_PrioridadViewModel> R_cat_Prioridad = await _cat_PrioridadRepository.GetCat_Prioridad_Seleccionar();
            ViewBag.Prioridad = R_cat_Prioridad;
            return View();
        }
    }
}