namespace ASD.Areas.Inventario.Models
{
    public class Cat_EquipoRutinaViewModel
    {
        public int Id { get; set; }
        public EquipoViewModel? Equipo { get; set; }
        public string? Nombre { get; set; }
        public int? Ordenamiento { get; set; }
        public string? Observaciones { get; set; }
    }
}
