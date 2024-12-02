using ASD.Areas.Empresa.Controllers;

namespace ASD.Areas.Empresa.Models
{
    public class CuadrillaZonaViewModel
    {
        public int Id { get; set; }
        public CuadrillaViewModel? Cuadrilla { get; set; }
        public Cat_ColoniaViewModel? Colonia { get; set; }
        public string? Observaciones { get; set; }
    }
}
