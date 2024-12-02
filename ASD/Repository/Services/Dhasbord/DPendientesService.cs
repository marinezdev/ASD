using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Dhasbord;

namespace ASD.Repository.Services.Dhasbord
{
    public class DPendientesService: IDPendientesRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public DPendientesService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<DPendientesViewModel> GetDhasbord_Pendientes_IdUsuario(UsuarioViewModel usuario)
        {
            DPendientesViewModel? _result = new DPendientesViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Pendientes_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<DPendientesViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<DPendientesViewModel> GetDhasbord_Pendientes_IdUsuario()
        {
            DPendientesViewModel? _result = new DPendientesViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Pendientes_IdUsuarioAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<DPendientesViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
