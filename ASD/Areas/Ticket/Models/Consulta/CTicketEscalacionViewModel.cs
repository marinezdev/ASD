﻿using ASD.Areas.Administracion.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Areas.Ticket.Models.Consulta
{
    public class CTicketEscalacionViewModel
    {
        public TicketEscalacionViewModel? TicketEscalacion { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public EmailViewModel? Email { get; set; }
        public TelefonoViewModel? Telefono { get; set; }
        public Cat_RolViewModel? Cat_Rol { get; set; }
    }
}
