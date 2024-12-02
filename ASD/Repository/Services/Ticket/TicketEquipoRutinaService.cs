using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class TicketEquipoRutinaService : ITicketEquipoRutinaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketEquipoRutinaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<TicketEquipoRutinaViewModel>> GetTicketEquipoRutina_Seleccionar_IdTicketEquipo(TicketEquipoViewModel ticketEquipo)
        {
            List<TicketEquipoRutinaViewModel>? _result = new List<TicketEquipoRutinaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipoRutina_Seleccionar_IdTicketEquipo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicketEquipo", ticketEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketEquipoRutinaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketEquipoRutinaViewModel> UpdateTicketEquipoRutina_Actualizar(TicketEquipoRutinaViewModel ticketEquipoRutina)
        {
            TicketEquipoRutinaViewModel? _result = new TicketEquipoRutinaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipoRutina_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticketEquipoRutina.Id);
                cmd.Parameters.AddWithValue("Estatus", ticketEquipoRutina.Estatus);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEquipoRutinaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
