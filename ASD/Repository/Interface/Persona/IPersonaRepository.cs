using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.TicketUnitario.Models;

namespace ASD.Repository.Interface.Persona
{
    public interface IPersonaRepository
    {
        Task<List<CPersonaViewModel>> GetPersonaListar();

        Task <PersonaViewModel> DeletePerson(PersonaViewModel persona);

        Task<List<Cat_TipoEmailViewModel>> GetCat_TipoEmailListar();
        Task<List<Cat_TipoTelefonoViewModel>> GetCat_TipoTelefonoListar();

        Task<CPersonaViewModel> Persona_listar_IdTicket(TicketAtenderViewModel ticket);

        Task<CPersonaViewModel> PersonaAtendio_listar_IdTicket(TicketAtenderViewModel ticket);

        Task<CPersonaViewModel> Persona_Seleccionar_IdUsuario(EquipoUsuarioViewModel usuario);

    }
}
