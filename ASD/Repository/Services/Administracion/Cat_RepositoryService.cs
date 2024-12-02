using System;
using ASD.Areas.Inventario.Models;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.Administracion;
using Newtonsoft.Json;
using ASD.Areas.Administracion.Models;

namespace ASD.Repository.Services.Administracion
{
	public class Cat_RepositoryService : ICat_RolRepository
	{
        private readonly string _cadenaSQL = string.Empty;

        public Cat_RepositoryService(IConfiguration configuration)
		{
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }


        public async Task<List<Cat_RolViewModel>> GetCat_Cat_Rol_Listar_IdUsuario(Cat_RolViewModel Cat_Rol)
        {
            List<Cat_RolViewModel>? _result = new List<Cat_RolViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Cat_Rol_Listar_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cat_Rol.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_RolViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}

