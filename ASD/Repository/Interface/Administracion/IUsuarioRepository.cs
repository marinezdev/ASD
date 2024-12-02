using ASD.Areas.Administracion.Models;
using ASD.Areas.Administracion.Models.Consulta;

namespace ASD.Repository.Interface.Administracion
{
	public interface IUsuarioRepository
	{
		Task<UsuariodbViewModel> GetAutentificar(UsuarioViewModel usuario);
		Task<CreateUserViewModel> SaveUsuario(CreateUserViewModel usuario);
        Task<CreateUserViewModel> UpdateUser(CreateUserViewModel usuario);
        Task<CreateUserViewModel> UpdateUserPass(CreateUserViewModel usuario);

        Task<List<Cat_RolViewModel>> ListarUsuarioRol();
        Task<CreateUserViewModel> GetUser_Id(CreateUserViewModel User);
    }
}
