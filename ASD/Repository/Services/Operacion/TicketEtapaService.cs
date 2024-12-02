using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Operacion.Models;
using ASD.Repository.Interface.Operacion;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Areas.Operacion.Models.Consulta;

namespace ASD.Repository.Services.Operacion
{
    public class TicketEtapaService : ITicketEtapaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;
        public TicketEtapaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<TicketEtapaViewModel>> GetTicketEtapa_Medidor(TicketViewModel ticket)
        {
            List<TicketEtapaViewModel>? _result = new List<TicketEtapaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.TicketEtapa_Medidor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketEtapaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<CTicketEtapaViewModel> GetTicketEtapa_Seleccionar_IdTicket(TicketViewModel ticket)
        {
            CTicketEtapaViewModel? _result = new CTicketEtapaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.TicketEtapa_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CTicketEtapaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<CTicketEtapa2ViewModel>> GetTicketEtapa_Consualta_IdTicket(TicketViewModel ticket)
        {
            List<CTicketEtapa2ViewModel>? _result = new List<CTicketEtapa2ViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.TicketEtapa_Consualta_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketEtapa2ViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


    }
}
