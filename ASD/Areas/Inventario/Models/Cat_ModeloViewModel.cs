namespace ASD.Areas.Inventario.Models
{
    public class Cat_ModeloViewModel
    {
        public int Id
        {
            get; set;
        }
        public Cat_CategoriaViewModel? Cat_Categoria { get; set; }
        public string? Nombre
        {
            get; set;
        }
        public DateTime? FechaRegistro { get; set; }
    }
}
