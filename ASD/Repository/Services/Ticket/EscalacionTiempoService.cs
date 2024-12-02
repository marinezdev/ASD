using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class EscalacionTiempoService : IEscalacionTiempoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;
        public EscalacionTiempoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<EscalacionTiempoViewModel> CreateEscalacionTiempo_Insertar(EscalacionTiempoViewModel escalacionTiempo)
        {
            EscalacionTiempoViewModel? _result = new EscalacionTiempoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.EscalacionTiempo_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", escalacionTiempo.Flujo.Id);
                cmd.Parameters.AddWithValue("Hora", escalacionTiempo.Hora);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EscalacionTiempoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<EscalacionTiempoViewModel>> GetEscalacionTiempo_Seleccionar_IdFlujo(EscalacionTiempoViewModel escalacionTiempo)
        {
            List<EscalacionTiempoViewModel>? _result = new List<EscalacionTiempoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.EscalacionTiempo_Seleccionar_IdFlujo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", escalacionTiempo.Flujo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EscalacionTiempoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<EscalacionTiempoViewModel> DeleteEscalacionTiempo_Eliminar(EscalacionTiempoViewModel escalacionTiempo)
        {
            EscalacionTiempoViewModel? _result = new EscalacionTiempoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.EscalacionTiempo_Eliminar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", escalacionTiempo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EscalacionTiempoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
