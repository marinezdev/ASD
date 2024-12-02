using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;

namespace ASD.Repository.Services.Empresa
{
    public class CuadrillaUsuarioService : ICuadrillaUsuarioRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public CuadrillaUsuarioService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<CuadrillaUsuarioViewModel>> GetCuadrillaUsuario_Seleccionar_IdCuadrilla(CuadrillaViewModel cuadrilla)
        {
            List<CuadrillaUsuarioViewModel>? _result = new List<CuadrillaUsuarioViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.CuadrillaUsuario_Seleccionar_IdCuadrilla", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdCuadrilla", cuadrilla.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CuadrillaUsuarioViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
