using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Services.Ticket
{
    public class TicketCuadrillaService : ITicketCuadrillaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketCuadrillaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<TicketCuadrillaViewModel> CreateTicketCuadrilla(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdCuadrilla", ticketCuadrilla.Cuadrilla.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Usuario.Id);
                cmd.Parameters.AddWithValue("IdUsuarioOP", ticketCuadrilla.Ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticketCuadrilla.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketCuadrillaViewModel> UpdateTicketCuadrilla(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdCuadrilla", ticketCuadrilla.Cuadrilla.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Usuario.Id);
                cmd.Parameters.AddWithValue("IdUsuarioOP", ticketCuadrilla.Ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticketCuadrilla.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_ProcesarEtapa(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_ProcesarEtapa", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticketCuadrilla.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_AsignarEtapa(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_AsignarEtapa", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_AtenderTicket(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_AtenderTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_ValidaAtencion(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_ValidaAtencion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketCuadrillaViewModel> CreateTicketCuadrilla_FinalizarTicket(TicketCuadrillaViewModel ticketCuadrilla)
        {
            TicketCuadrillaViewModel? _result = new TicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_FinalizarTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketCuadrilla.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticketCuadrilla.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<CTicketCuadrillaViewModel> GetTicketCuadrilla_Seleccionar_IdTicket(TicketViewModel ticket)
        {
            CTicketCuadrillaViewModel? _result = new CTicketCuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketCuadrilla_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CTicketCuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
