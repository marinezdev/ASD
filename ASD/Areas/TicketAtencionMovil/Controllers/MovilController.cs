using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Controllers;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Repository.Interface.Empresa;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Security.Policy;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.TicketAtencion.Controllers
{
    [Authorize]
    [Area("TicketAtencionMovil")]
    public class MovilController : Controller
    {
        private readonly ILogger<MovilController> _logger;
        private readonly ITicketEtapaRepository _ticketEtapaRepository;
        private readonly ITicketCuadrillaRepository _ticketCuadrillaRepository;

        public MovilController(ILogger<MovilController> logger, ITicketEtapaRepository ticketEtapaRepository, ITicketCuadrillaRepository ticketCuadrillaRepository)
        {
            _logger = logger;
            _ticketEtapaRepository = ticketEtapaRepository;
            _ticketCuadrillaRepository = ticketCuadrillaRepository;
        }

        [HttpGet()]
        public virtual async Task<IActionResult> Index([FromQuery(Name = "cont")] string IdC)
        {
            int IdN = Convert.ToInt32(Cifrado.Desencriptar(IdC));

            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            TicketViewModel ticket = new TicketViewModel();
            ticket.Id = Convert.ToInt32(IdN);

            if (claimuser.Identity.IsAuthenticated)
            {
               var Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }

            TicketCuadrillaViewModel ticketCuadrilla = new TicketCuadrillaViewModel();
            ticketCuadrilla.Ticket = ticket;
            ticketCuadrilla.Usuario = usuario;
            TicketCuadrillaViewModel dbticketCuadrilla = await _ticketCuadrillaRepository.CreateTicketCuadrilla_ValidaAtencion(ticketCuadrilla);

            if (dbticketCuadrilla.Id > 0)
            {
                return RedirectToAction("Index", "Informacion", new { area = "TicketAtencionMovil", @cont = IdC });

            }
            else
            {
                CTicketEtapaViewModel dbTicket = await _ticketEtapaRepository.GetTicketEtapa_Seleccionar_IdTicket(ticket);

                ViewBag.TicketEtapa = dbTicket;
                ViewBag.Usuario = usuario;
                return View();
            }
        }
    }
}
