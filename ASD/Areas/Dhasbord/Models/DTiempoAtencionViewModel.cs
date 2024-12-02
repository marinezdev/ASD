using ASD.Areas.Operacion.Models;

namespace ASD.Areas.Dhasbord.Models
{
    public class DTiempoAtencionViewModel
    {
        public EtapaViewModel? Etapa { get; set; }
        public int Tickets { get; set; }
        public int Tiempo { get; set; }
        public int TiempoAtencionSegundos { get; set; }
        public string? TiempoAtencion { get; set; }
    }
}
