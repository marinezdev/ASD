using ASD.Areas.Operacion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Persona.Models;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Operacion;

namespace ASD.Repository.Services.Operacion
{
    public class UsuarioFlujoService : IUsuarioFlujoRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public UsuarioFlujoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<EmailViewModel>> GetUsuarioEtapa_Notificacion_Operacion(TicketViewModel ticket, string Etapa)
        {
            List<EmailViewModel> _result = new List<EmailViewModel>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.UsuarioEtapa_Notificacion_Operacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("NombreEtapa", Etapa);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EmailViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
