namespace ASD.Areas.Operacion.Models
{
    public class EtapaViewModel
    {
        public int Id { get; set; }
        public FlujoViewModel? Flujo { get; set; }
        public string? Nombre { get; set; }
        public int Etapa { get; set; }
        public string? Color { get; set; }

    }
}
