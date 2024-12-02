using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class TicketEscalacionService : ITicketEscalacionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketEscalacionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<TicketEscalacionViewModel> CreateTicketEscalacion(TicketEscalacionViewModel ticketEscalacion)
        {
            TicketEscalacionViewModel? _result = new TicketEscalacionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEscalacion_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketEscalacion.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketEscalacion.Usuario.Id);
                cmd.Parameters.AddWithValue("Dias", ticketEscalacion.Dias);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEscalacionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<CTicketEscalacionViewModel>> GetTicketEscalacion_Seleccionar_IdTicket(TicketEscalacionViewModel ticketEscalacion)
        {
            List<CTicketEscalacionViewModel>? _result = new List<CTicketEscalacionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEscalacion_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketEscalacion.Ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketEscalacionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<TicketEscalacionViewModel> DeleteTicketEscalacion(TicketEscalacionViewModel ticketEscalacion)
        {
            TicketEscalacionViewModel? _result = new TicketEscalacionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEscalacion_Eliminar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticketEscalacion.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEscalacionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
