using ASD.Areas.Persona.Models;

namespace ASD.Areas.Administracion.Models
{
    public class PasswordResetRequestsViewModel
    {
        public int? Id { get; set; }
        public string? Token { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public EmailViewModel? Email { get; set; }
        public string? Direccion { get; set; }
    }
}
