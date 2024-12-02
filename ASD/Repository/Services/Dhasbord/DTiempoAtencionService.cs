using ASD.Areas.Dhasbord.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Dhasbord;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Services.Dhasbord
{
    public class DTiempoAtencionService : IDTiempoAtencionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public DTiempoAtencionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<DTiempoAtencionViewModel>> GetDhasbord_Tiempos_Atencion(UsuarioViewModel usuario)
        {
            List<DTiempoAtencionViewModel>? _result = new List<DTiempoAtencionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Tiempos_Atencion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DTiempoAtencionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<DTiempoAtencionViewModel>> GetDhasbord_Tiempos_Atencion_Ticket(TicketViewModel ticket)
        {
            List<DTiempoAtencionViewModel>? _result = new List<DTiempoAtencionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Tiempos_Atencion_Ticket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DTiempoAtencionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetDhasbord_Tiempos_Atencion_Tickets(UsuarioViewModel usuario, EtapaViewModel etapa)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Tiempos_Atencion_Tickets", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Etapa", etapa.Nombre);
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }



        public async Task<List<DTiempoAtencionViewModel>> GetDhasbord_Tiempos_Atencion()
        {
            List<DTiempoAtencionViewModel>? _result = new List<DTiempoAtencionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Tiempos_AtencionAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DTiempoAtencionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


    }
}
