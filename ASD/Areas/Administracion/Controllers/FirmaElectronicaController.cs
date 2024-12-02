using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Repository.Interface.Administracion;
using ASD.Areas.Administracion.Models;
using ASD.Areas.TicketAtencionMovil.Models;
using System.Security.Claims;
using ASD.Repository.Interface.TicketAtencionMovil;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Dhasbord.Models;

namespace ASD.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    [Authorize]

    public class FirmaElectronicaController : Controller
    {
        private readonly IFirmaElectronicaRepository _firmaElectronicaRepository;

        public FirmaElectronicaController(IFirmaElectronicaRepository firmaElectronicaRepository)
        {
            _firmaElectronicaRepository = firmaElectronicaRepository;

        }

        [HttpPost]
        public async Task<JsonResult> CrearFirma([FromBody] FirmaElectronicaViewModel firmaElectronica)
        {
           FirmaElectronicaViewModel R_Firma = await _firmaElectronicaRepository.CreateFirma(firmaElectronica);
            return Json(R_Firma);
        }

        [HttpPost]
        public async Task<JsonResult> Create_Signature([FromBody] InformacionAdicionalViewModel InformacionAdicional)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string IdUser;

            if (claimuser.Identity.IsAuthenticated)
            {
                IdUser = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(IdUser);
            }

            InformacionAdicional.Usuario = usuario;
            InformacionAdicionalViewModel Result = await _firmaElectronicaRepository.Create_Signature(InformacionAdicional);
            return Json(Result);
        }

        [HttpPost]
        public async Task<JsonResult> Delete_Firma([FromBody] InformacionAdicionalViewModel InformacionAdicional)
        {
            InformacionAdicionalViewModel Result = await _firmaElectronicaRepository.Delete_Firma(InformacionAdicional);
            return Json(Result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateIDC_Firma([FromBody] InformacionAdicionalViewModel InformacionAdicional)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                usuario.Id = Convert.ToInt32(claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            }
            InformacionAdicional.Usuario = usuario;

            InformacionAdicionalViewModel Result = await _firmaElectronicaRepository.UpdateIDC_Firma(InformacionAdicional);
            return Json(Result);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateCliente_Firma([FromBody] InformacionAdicionalViewModel InformacionAdicional)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;

            if (claimuser.Identity.IsAuthenticated)
            {
                usuario.Id = Convert.ToInt32(claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault());
            }
            InformacionAdicional.Usuario = usuario;

            InformacionAdicionalViewModel Result = await _firmaElectronicaRepository.UpdateCliente_Firma(InformacionAdicional);
            return Json(Result);
        }

        [HttpPost]
        public async Task<JsonResult> Ticket_Procesar_Supervisor_Signature([FromBody] SignatureTicketViewModel ticket)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            ClaimsPrincipal claimuser = HttpContext.User;
            string Id;

            if (claimuser.Identity.IsAuthenticated)
            {
                Id = claimuser.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault();
                usuario.Id = Convert.ToInt32(Id);
            }
            ticket.Usuario = usuario;
            SignatureTicketViewModel db_tickets = await _firmaElectronicaRepository.Ticket_Procesar_Supervisor_Signature(ticket);
            return Json(db_tickets);
        }
    }
}