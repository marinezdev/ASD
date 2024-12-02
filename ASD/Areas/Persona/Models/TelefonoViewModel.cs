namespace ASD.Areas.Persona.Models
{
    public class TelefonoViewModel
    {
        public int Id { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public Cat_TipoTelefonoViewModel? Cat_TipoTelefono { get; set; }
        public string? Telefono { get; set; }
    }
}
