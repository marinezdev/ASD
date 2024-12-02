namespace ASD.Areas.Persona.Models
{
    public class EmailViewModel
    {
        public int Id { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public Cat_TipoEmailViewModel? Cat_TipoEmail { get; set; }
        public string? Email { get; set; }
    }
}
