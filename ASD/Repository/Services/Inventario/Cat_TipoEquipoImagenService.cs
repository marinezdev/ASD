using ASD.Areas.Inventario.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Inventario;

namespace ASD.Repository.Services.Inventario
{
    public class Cat_TipoEquipoImagenService : ICat_TipoEquipoImagenRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_TipoEquipoImagenService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_TipoEquipoImagenViewModel>> GetCat_TipoEquipoImagen(Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_TipoEquipoImagenViewModel>? _result = new List<Cat_TipoEquipoImagenViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_TipoEquipoImagen_Seleccionar_TipoEquipo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTipoEquipo", cat_TipoEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_TipoEquipoImagenViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }







    }
}
