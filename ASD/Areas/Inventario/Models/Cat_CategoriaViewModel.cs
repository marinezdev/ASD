namespace ASD.Areas.Inventario.Models
{
    public class Cat_CategoriaViewModel
    {
        public int Id {get; set;}
        public Cat_TipoEquipoViewModel? Cat_TipoEquipo { get; set; }
        public string? Nombre{ get; set;}
        public DateTime? FechaRegistro { get; set; }
    }
}
