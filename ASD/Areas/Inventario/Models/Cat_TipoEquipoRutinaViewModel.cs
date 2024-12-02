namespace ASD.Areas.Inventario.Models
{
    public class Cat_TipoEquipoRutinaViewModel
    {
        public int Id { get; set; }
        public Cat_TipoEquipoViewModel? Cat_TipoEquipo { get; set; }
        public string? Nombre { get; set; }
        public int? Ordenamiento { get; set; }
        public string? Observaciones { get; set; }
    }
}
