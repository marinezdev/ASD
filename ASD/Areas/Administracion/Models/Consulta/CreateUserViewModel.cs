using System;
using ASD.Areas.Persona.Models;

namespace ASD.Areas.Administracion.Models.Consulta
{
	public class CreateUserViewModel
	{
        public int Id { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public EmailViewModel? Email { get; set; }
        public TelefonoViewModel? Telefono { get; set; }

    }
}

