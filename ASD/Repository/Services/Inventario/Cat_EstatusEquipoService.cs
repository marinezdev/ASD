using ASD.Areas.Inventario.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Inventario;

namespace ASD.Repository.Services.Inventario
{
    public class Cat_EstatusEquipoService : ICat_EstatusEquipoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EstatusEquipoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_EstatusEquipoViewModel>> GetCat_EstatusEquipo_Seleccionar()
        {
            List<Cat_EstatusEquipoViewModel>? _result = new List<Cat_EstatusEquipoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_EstatusEquipo_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_EstatusEquipoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
