using ASD.Areas.Administracion.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Areas.Ticket.Models.Consulta
{
    public class CBitacoraViewModel
    {
        public BitacoraViewModel? Bitacora { get; set; }
        public PersonaViewModel? Persona { get; set; }
        public Cat_OperacionViewModelcs? Cat_Operacion { get; set; }
    }
}
