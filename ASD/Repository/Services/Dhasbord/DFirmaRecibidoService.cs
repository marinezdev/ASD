using ASD.Areas.Administracion.Models;
using ASD.Areas.Dhasbord.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Dhasbord;

namespace ASD.Repository.Services.Dhasbord
{
    public class DFirmaRecibidoService : IDFirmaRecibidoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public DFirmaRecibidoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta1(UsuarioViewModel usuario)
        {
            List<FirmaRecibidoViewModel>? _result = new List<FirmaRecibidoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Recibido_Encuesta_Pregunta1", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<FirmaRecibidoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta2(UsuarioViewModel usuario)
        {
            List<FirmaRecibidoViewModel>? _result = new List<FirmaRecibidoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Recibido_Encuesta_Pregunta2", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<FirmaRecibidoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta3(UsuarioViewModel usuario)
        {
            List<FirmaRecibidoViewModel>? _result = new List<FirmaRecibidoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Recibido_Encuesta_Pregunta3", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<FirmaRecibidoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta1()
        {
            List<FirmaRecibidoViewModel>? _result = new List<FirmaRecibidoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Recibido_Encuesta_Pregunta1Admin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<FirmaRecibidoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta2()
        {
            List<FirmaRecibidoViewModel>? _result = new List<FirmaRecibidoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Recibido_Encuesta_Pregunta2Admin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<FirmaRecibidoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta3()
        {
            List<FirmaRecibidoViewModel>? _result = new List<FirmaRecibidoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Dhasbord.Dhasbord_Recibido_Encuesta_Pregunta3Admin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<FirmaRecibidoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
