using ASD.Areas.Dhasbord.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Administracion.Models;
using ASD.Repository.Interface.Dhasbord;

namespace ASD.Repository.Services.Dhasbord
{
    public class DPrioridadService: IDPrioridadRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public DPrioridadService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<DPrioridadViewModel>> GetDhasbord_Prioridad_Operacion(UsuarioViewModel usuario)
        {
            List<DPrioridadViewModel>? _result = new List<DPrioridadViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Prioridad_Operacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DPrioridadViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<DPrioridadViewModel>> GetCliente_Ticket_Prioridad(UsuarioViewModel usuario)
        {
            List<DPrioridadViewModel>? _result = new List<DPrioridadViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Cliente_Ticket_Prioridad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DPrioridadViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<DPrioridadViewModel>> GetDhasbord_Prioridad_Operacion()
        {
            List<DPrioridadViewModel>? _result = new List<DPrioridadViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Prioridad_OperacionAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<DPrioridadViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
