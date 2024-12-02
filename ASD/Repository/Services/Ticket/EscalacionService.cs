using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Repository.Interface.Ticket;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ASD.Repository.Services.Ticket
{
    public class EscalacionService : IEscalacionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public EscalacionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<EscalacionViewModel> CreateEscalacion_Insertar(EscalacionViewModel escalacion)
        {
            EscalacionViewModel? _result = new EscalacionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Escalacion_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", escalacion.Usuario.Id);
                cmd.Parameters.AddWithValue("IdFlujo", escalacion.Flujo.Id);
                cmd.Parameters.AddWithValue("Dias", escalacion.Dias);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EscalacionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<CEscalacionViewModel>> GetEscalacion_Seleccionar_IdFlujo(EscalacionViewModel escalacion)
        {
            List<CEscalacionViewModel>? _result = new List<CEscalacionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Escalacion_Seleccionar_IdFlujo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", escalacion.Flujo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CEscalacionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<EscalacionViewModel> DeleteEscalacion_Eliminar(EscalacionViewModel escalacion)
        {
            EscalacionViewModel? _result = new EscalacionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Escalacion_Eliminar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", escalacion.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EscalacionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
