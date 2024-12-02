using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;

namespace ASD.Repository.Services.Empresa
{
    public class Cat_TipoSucursalService : ICat_TipoSucursalRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_TipoSucursalService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<Cat_TipoSucursalViewModel>> GetCat_TipoSucursal_Seleccionar()
        {
            List<Cat_TipoSucursalViewModel>? _result = new List<Cat_TipoSucursalViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_TipoSucursal_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_TipoSucursalViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
