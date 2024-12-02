namespace ASD.Areas.Empresa.Models
{
    public class Cat_PoblacionViewModel
    {
        public int Id { get; set; }
        public Cat_EstadoViewModel? Cat_Estado { get; set; }
        public string? Nombre { get; set; }
    }
}
