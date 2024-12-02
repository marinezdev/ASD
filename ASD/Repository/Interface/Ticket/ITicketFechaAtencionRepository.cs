using System;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Ticket
{
	public interface ITicketFechaAtencionRepository
	{
        Task<List<CTicketFechaAtencionViewModel>> Ticket_FechaAtencion_Seleccionar_IdUser(CTicketFechaAtencionViewModel usuario);

    }
}

