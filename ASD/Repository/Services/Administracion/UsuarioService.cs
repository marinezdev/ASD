using ASD.Areas.Administracion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Administracion;
using ASD.Areas.Administracion.Models.Consulta;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Services.Administracion
{
	public class UsuarioService :IUsuarioRepository
	{
		private readonly string _cadenaSQL = string.Empty;

		public UsuarioService(IConfiguration configuration)
		{
			_cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
		}

		public async Task<UsuariodbViewModel> GetAutentificar(UsuarioViewModel usuario)
		{
			UsuariodbViewModel _result = new Areas.Administracion.Models.Consulta.UsuariodbViewModel();

			using (var conexion = new SqlConnection(_cadenaSQL))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("Administracion.Usuario_Autentificar", conexion);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("Usuario", usuario.Usuario);
				cmd.Parameters.AddWithValue("Password", usuario.Password);

				using (var dr = await cmd.ExecuteReaderAsync())
				{
					if (await dr.ReadAsync())
					{
						_result = JsonConvert.DeserializeObject<UsuariodbViewModel>(dr.GetValue(0).ToString());
					}
				}
				conexion.Close();
				return _result;
			}
		}

		public async Task<CreateUserViewModel> SaveUsuario(CreateUserViewModel usuario)
		{

            CreateUserViewModel? _result = new CreateUserViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Usuario_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdRol", usuario.Usuario.Cat_Rol.Id);
                cmd.Parameters.AddWithValue("Password", usuario.Usuario.Password);
                cmd.Parameters.AddWithValue("Usuario", usuario.Usuario.Usuario);
                cmd.Parameters.AddWithValue("Nombre", usuario.Persona.Nombre);
                cmd.Parameters.AddWithValue("ApellidoPaterno", usuario.Persona.ApellidoPaterno);
                cmd.Parameters.AddWithValue("ApellidoMaterno", usuario.Persona.ApellidoMaterno);
                cmd.Parameters.AddWithValue("TipoEmail", usuario.Email.Cat_TipoEmail.Id);
                cmd.Parameters.AddWithValue("TipoTelefono", usuario.Telefono.Cat_TipoTelefono.Id);
                cmd.Parameters.AddWithValue("Telefono", usuario.Telefono.Telefono);
                cmd.Parameters.AddWithValue("Cuadrilla", usuario.Usuario.Cuadrilla.Id);



                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CreateUserViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
            
        }

        public async Task<CreateUserViewModel> GetUser_Id(CreateUserViewModel usuario)
        {
            List<CreateUserViewModel> _users = new List<CreateUserViewModel>();
            CreateUserViewModel usuarioViewModel = new CreateUserViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Persona.persona_listar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _users = JsonConvert.DeserializeObject<List<CreateUserViewModel>>(dr.GetValue(0).ToString());
                    }
                }

                usuarioViewModel = _users.Where(item => item.Usuario.Id == usuario.Usuario.Id).FirstOrDefault();

                return usuarioViewModel;
            }
        }

        public async Task<CreateUserViewModel> UpdateUser(CreateUserViewModel usuario)
        {

            CreateUserViewModel? _result = new CreateUserViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Usuario_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdRol", usuario.Usuario.Cat_Rol.Id);
                cmd.Parameters.AddWithValue("Id", usuario.Usuario.Id);
                cmd.Parameters.AddWithValue("Usuario", usuario.Usuario.Usuario);
                cmd.Parameters.AddWithValue("Nombre", usuario.Persona.Nombre);
                cmd.Parameters.AddWithValue("ApellidoPaterno", usuario.Persona.ApellidoPaterno);
                cmd.Parameters.AddWithValue("ApellidoMaterno", usuario.Persona.ApellidoMaterno);
                cmd.Parameters.AddWithValue("TipoEmail", usuario.Email.Cat_TipoEmail.Id);
                cmd.Parameters.AddWithValue("TipoTelefono", usuario.Telefono.Cat_TipoTelefono.Id);
                cmd.Parameters.AddWithValue("Telefono", usuario.Telefono.Telefono);
                cmd.Parameters.AddWithValue("Cuadrilla", usuario.Usuario.Cuadrilla.Id);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CreateUserViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<List<Cat_RolViewModel>> ListarUsuarioRol()
        {
            List<Cat_RolViewModel> _users = new List<Cat_RolViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Cat_Rol_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _users = JsonConvert.DeserializeObject<List<Cat_RolViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _users;
            }
        }


        public async Task<CreateUserViewModel> UpdateUserPass(CreateUserViewModel usuario)
        {

            CreateUserViewModel? _result = new CreateUserViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Usuario_ActualizarPass", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Id", usuario.Usuario.Id);
                cmd.Parameters.AddWithValue("Password", usuario.Usuario.Password);



                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CreateUserViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
