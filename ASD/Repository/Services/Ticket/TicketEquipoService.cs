using ASD.Areas.Ticket.Models.Consulta;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Services.Ticket
{
    public class TicketEquipoService : ITicketEquipoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketEquipoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<TicketEquipoViewModel> CreateTicketEquipo(TicketEquipoViewModel ticketEquipo)
        {
            TicketEquipoViewModel? _result = new TicketEquipoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketEquipo.Ticket.Id);
                cmd.Parameters.AddWithValue("IdEquipo", ticketEquipo.Equipo.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticketEquipo.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEquipoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketEquipoViewModel>> GetTicketEquipo_Seleccionar_IdTicket(TicketViewModel ticket)
        {
            List<CTicketEquipoViewModel>? _result = new List<CTicketEquipoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipo_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketEquipoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }







        public async Task<EquipoViewModel> TicketEquipo_CrearSD(EquipoViewModel Equipo)
        {
            EquipoViewModel? _result = new EquipoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipo_CrearSD", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicketEquipo", Equipo.cat_TipoEquipo.Id);
                cmd.Parameters.AddWithValue("IdEquipo", Equipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EquipoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
