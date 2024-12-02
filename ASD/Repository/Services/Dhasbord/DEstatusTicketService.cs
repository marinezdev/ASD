using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Dhasbord;

namespace ASD.Repository.Services.Dhasbord
{
    public class DEstatusTicketService : IDEstatusTicketRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public DEstatusTicketService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<DTicketViewModel>> GetCliente_Ticket_EstatusTicket(UsuarioViewModel usuario)
        {
            List<DTicketViewModel>? _result = new List<DTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Cliente_Ticket_EstatusTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
