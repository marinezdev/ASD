using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models.Consulta;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_TipoServicioService : ICat_TipoServicioRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_TipoServicioService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<Cat_TipoServicioViewModel>> GetCat_TipoServicio_Seleccionar_IdFlujo(FlujoViewModel flujo)
        {
            List<Cat_TipoServicioViewModel>? _result = new List<Cat_TipoServicioViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_TipoServicio_Seleccionar_IdFlujo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", flujo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_TipoServicioViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_TipoServicioViewModel> GetCat_TipoServicio_Seleccionar_Id(Cat_TipoServicioViewModel cat_TipoServicio)
        {
            Cat_TipoServicioViewModel? _result = new Cat_TipoServicioViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_TipoServicio_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoServicio.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoServicioViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
