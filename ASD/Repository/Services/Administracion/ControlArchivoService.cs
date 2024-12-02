using ASD.Areas.Administracion.Models.Consulta;
using ASD.Areas.Administracion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Administracion;

namespace ASD.Repository.Services.Administracion
{
    public class ControlArchivoService : IControlArchivoRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public ControlArchivoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<ControlArchivoViewModel> CreateControlArchivo()
        {
            ControlArchivoViewModel _result = new ControlArchivoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.ControlArchivo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<ControlArchivoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
