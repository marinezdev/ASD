using System;
using ASD.Areas.Administracion.Models;

namespace ASD.Areas.TicketAtencionMovil.Models
{
	public class InformacionAdicionalViewModel
	{
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public string? Firma { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
    }
}

