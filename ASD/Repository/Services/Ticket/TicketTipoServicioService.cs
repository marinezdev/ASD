using ASD.Areas.Ticket.Models.Consulta;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class TicketTipoServicioService : ITicketTipoServicioRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketTipoServicioService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<TicketTipoServicioViewModel> CreateTicketTipoServicio(TicketTipoServicioViewModel ticketTipoServicio)
        {
            TicketTipoServicioViewModel? _result = new TicketTipoServicioViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketTipoServicio_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketTipoServicio.Ticket.Id);
                cmd.Parameters.AddWithValue("IdTipoServicio", ticketTipoServicio.Cat_TipoServicio.Id);
                cmd.Parameters.AddWithValue("Valor", ticketTipoServicio.Valor);
                cmd.Parameters.AddWithValue("Observaciones", ticketTipoServicio.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketTipoServicioViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
