using ASD.Areas.Ticket.Models.Consulta;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class NuevaFechaAtencionService : INuevaFechaAtencionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public NuevaFechaAtencionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<NuevaFechaAtencionViewModel> CreateNuevaFechaAtencion_Crear(NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            NuevaFechaAtencionViewModel? _result = new NuevaFechaAtencionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.NuevaFechaAtencion_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", nuevaFechaAtencion.Usuario.Id);
                cmd.Parameters.AddWithValue("IdTicket", nuevaFechaAtencion.Ticket.Id);
                cmd.Parameters.AddWithValue("FechaAtencion", nuevaFechaAtencion.FechaAtencion);
                cmd.Parameters.AddWithValue("Observaciones", nuevaFechaAtencion.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<NuevaFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<NuevaFechaAtencionViewModel> CreateNuevaFechaAtencion_Crear_Super(NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            NuevaFechaAtencionViewModel? _result = new NuevaFechaAtencionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.NuevaFechaAtencion_Crear_Super", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", nuevaFechaAtencion.Usuario.Id);
                cmd.Parameters.AddWithValue("IdTicket", nuevaFechaAtencion.Ticket.Id);
                cmd.Parameters.AddWithValue("FechaAtencion", nuevaFechaAtencion.FechaAtencion);
                cmd.Parameters.AddWithValue("Observaciones", nuevaFechaAtencion.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<NuevaFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<NuevaFechaAtencionViewModel> UpdateNuevaFechaAtencion_Aceptar(NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            NuevaFechaAtencionViewModel? _result = new NuevaFechaAtencionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.NuevaFechaAtencion_Aceptar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", nuevaFechaAtencion.Id);
                cmd.Parameters.AddWithValue("IdUsuario", nuevaFechaAtencion.Usuario.Id);
                
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<NuevaFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<CNuevaFechaAtencionViewModel> GetNuevaFechaAtencion_Seleccionar_IdTicket(TicketViewModel ticket)
        {
            CNuevaFechaAtencionViewModel? _result = new CNuevaFechaAtencionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.NuevaFechaAtencion_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id); 

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CNuevaFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<NuevaFechaAtencionViewModel> CreateNuevaFechaAtencion_Crear_TicketUnitario(NuevaFechaAtencionViewModel nuevaFechaAtencion)
        {
            NuevaFechaAtencionViewModel? _result = new NuevaFechaAtencionViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.NuevaFechaAtencion_Crear_TicketUnitario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", nuevaFechaAtencion.Usuario.Id);
                cmd.Parameters.AddWithValue("IdTicket", nuevaFechaAtencion.Ticket.Id);
                cmd.Parameters.AddWithValue("FechaAtencion", nuevaFechaAtencion.FechaAtencion);
                cmd.Parameters.AddWithValue("Observaciones", nuevaFechaAtencion.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<NuevaFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
