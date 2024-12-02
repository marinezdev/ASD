using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Services.Empresa
{
    public class Cat_EstadoService : ICat_EstadoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EstadoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<Cat_EstadoViewModel>> GetCat_Estado_Seleccionar()
        {
            List<Cat_EstadoViewModel>? _result = new List<Cat_EstadoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_Estado_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_EstadoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstadoViewModel> CreateCat_Estado(Cat_EstadoViewModel cat_Estado)
        {
            Cat_EstadoViewModel? _result = new Cat_EstadoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cat_Estado_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", cat_Estado.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync()) 
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstadoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
