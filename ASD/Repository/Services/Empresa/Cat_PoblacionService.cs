using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;

namespace ASD.Repository.Services.Empresa
{
    public class Cat_PoblacionService : ICat_PoblacionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_PoblacionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        
        public async Task<List<Cat_PoblacionViewModel>> GetCat_Poblacion_Seleccionar_IdEstado(Cat_EstadoViewModel cat_Estado)
        {
            List<Cat_PoblacionViewModel>? _result = new List<Cat_PoblacionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_Poblacion_Seleccionar_IdEstado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEstado", cat_Estado.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_PoblacionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_PoblacionViewModel> CreateCat_Poblacion(Cat_PoblacionViewModel cat_Poblacion)
        {
            Cat_PoblacionViewModel? _result = new Cat_PoblacionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_Poblacion_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEstado", cat_Poblacion.Cat_Estado.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_Poblacion.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_PoblacionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
