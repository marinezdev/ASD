using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Persona.Models.Consulta
{
    public class CPersonaViewModel
    {
        public PersonaViewModel? Persona { get; set; }
        public EmailViewModel? Email { get; set; }
        public TelefonoViewModel? Telefono { get; set; }
        public Cat_RolViewModel? CatRol { get; set; }

        public UsuarioViewModel? Usuario { get; set; }
    }
}
