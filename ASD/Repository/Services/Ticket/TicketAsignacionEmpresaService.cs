using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class TicketAsignacionEmpresaService : ITicketAsignacionEmpresaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketAsignacionEmpresaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<TicketAsignacionEmpresaViewModel> CreateTicketAsignacionEmpresa(TicketAsignacionEmpresaViewModel ticketAsignacionEmpresa)
        {
            TicketAsignacionEmpresaViewModel? _result = new TicketAsignacionEmpresaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketAsignacionEmpresa_Agregar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticketAsignacionEmpresa.Ticket.Id);
                cmd.Parameters.AddWithValue("IdEmpresa", ticketAsignacionEmpresa.Cat_AsignacionEmpresa.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketAsignacionEmpresaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
