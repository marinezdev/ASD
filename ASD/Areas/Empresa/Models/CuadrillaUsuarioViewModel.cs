using ASD.Areas.Administracion.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Areas.Empresa.Models
{
    public class CuadrillaUsuarioViewModel
    {
        public PersonaViewModel? Persona { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
    }
}
