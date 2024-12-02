using ASD.Areas.Persona.Models;

namespace ASD.Areas.Operacion.Models.Consulta
{
    public class CTicketEtapa2ViewModel
    {
        public EtapaViewModel? Etapa { get; set; }
        public TicketEtapaViewModel? TicketEtapa { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public string? TiempoAtencion { get; set; }
    }
}
