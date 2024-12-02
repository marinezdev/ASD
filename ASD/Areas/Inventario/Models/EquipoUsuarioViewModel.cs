using ASD.Areas.Administracion.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Areas.Inventario.Models
{
    public class EquipoUsuarioViewModel
    {
        public int Id { get; set; }
        public EquipoViewModel? Equipo { get; set; }
        public UsuarioViewModel? Usuario { get; set; } 
        public PersonaViewModel? Persona { get; set; } 
        public EmailViewModel? Email { get; set; }
    }
}
