using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Inventario;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ASD.Repository.Services.Inventario
{
    public class Cat_TipoEquipoRutinaService : ICat_TipoEquipoRutinaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_TipoEquipoRutinaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_TipoEquipoRutinaViewModel>> GetCat_TipoEquipoRutina(Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_TipoEquipoRutinaViewModel>? _result = new List<Cat_TipoEquipoRutinaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_TipoEquipoRutina_Seleccionar_TipoEquipo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTipoEquipo", cat_TipoEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_TipoEquipoRutinaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
