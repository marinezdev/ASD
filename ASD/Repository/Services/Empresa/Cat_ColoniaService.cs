using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;

namespace ASD.Repository.Services.Empresa
{
    public class Cat_ColoniaService : ICat_ColoniaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_ColoniaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_ColoniaViewModel>> GetCat_Colonia_Seleccionar_IdCP(Cat_CPViewModel cat_CP)
        {
            List<Cat_ColoniaViewModel>? _result = new List<Cat_ColoniaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_Colonia_Seleccionar_IdCP", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdCP", cat_CP.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_ColoniaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_ColoniaViewModel> CreateCat_Colonia(Cat_ColoniaViewModel cat_Colonia)
        {
            Cat_ColoniaViewModel? _result = new Cat_ColoniaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_Colonia_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdCP", cat_Colonia.Cat_CP.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_Colonia.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_ColoniaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
