using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;

namespace ASD.Repository.Services.Empresa
{
    public class Cat_CPService : ICat_CPRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_CPService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_CPViewModel>> GetCat_CP_Seleccionar_IdPoblacion(Cat_PoblacionViewModel cat_Poblacion)
        {
            List<Cat_CPViewModel>? _result = new List<Cat_CPViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_CP_Seleccionar_IdPoblacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdPoblacion", cat_Poblacion.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_CPViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_CPViewModel> CreateCat_CP(Cat_CPViewModel cat_CP)
        {
            Cat_CPViewModel? _result = new Cat_CPViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_CP_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdPoblacion", cat_CP.Cat_Poblacion.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_CP.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_CPViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
