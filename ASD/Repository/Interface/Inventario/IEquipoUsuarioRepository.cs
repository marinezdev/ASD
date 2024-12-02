using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Interface.Inventario
{
    public interface IEquipoUsuarioRepository
    {

        Task<List<EquipoUsuarioViewModel>> GetPersonaListar();
        Task<List<EquipoViewModel>> GetEquipoUsuario_Seleccionar_IdUsuario(UsuarioViewModel usuario);
        Task<EquipoUsuarioViewModel> CreateEquipoUsuario(EquipoUsuarioViewModel equipoUsuario);

        Task<List<EquipoViewModel>> GetEquipo_Seleccionar_Id(EquipoUsuarioViewModel usuario);

    }
}
