using ASD.Areas.Operacion.Models;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.Operacion;


namespace ASD.Repository.Services.Operacion
{
    public class UsuarioEtapaService: IUsuarioEtapaRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public UsuarioEtapaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<UsuarioEtapaViewModel>> Etapa_Listar_IdUsuario(UsuarioEtapaViewModel Flujo)
        {
            List<UsuarioEtapaViewModel> _Flujo = new List<UsuarioEtapaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Etapa_Listar_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", Flujo.Id);
                cmd.Parameters.AddWithValue("Id", Flujo.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _Flujo = JsonConvert.DeserializeObject<List<UsuarioEtapaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _Flujo;
            }
        }


        public async Task<UsuarioEtapaViewModel> Delete_Etapa(UsuarioEtapaViewModel Etapa)
        {
            UsuarioEtapaViewModel _result = new UsuarioEtapaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.UsuarioEtapa_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Etapa.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<UsuarioEtapaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }


        public async Task<UsuarioEtapaViewModel> Add_UsuarioEtapa(UsuarioEtapaViewModel Etapa)
        {
            UsuarioEtapaViewModel _result = new UsuarioEtapaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.UsuarioEtapa_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", Etapa.Id);
                cmd.Parameters.AddWithValue("IdEtapa", Etapa.Etapa.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<UsuarioEtapaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<List<UsuarioEtapaViewModel>> GetCat_Etapa_Listar_Faltante(UsuarioFlujoViewModel UsuarioFlujo)
        {
            List<UsuarioEtapaViewModel> _Flujo = new List<UsuarioEtapaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Etapa_Listar_Faltante", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", UsuarioFlujo.Id);
                cmd.Parameters.AddWithValue("IdFlujo", UsuarioFlujo.Flujo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _Flujo = JsonConvert.DeserializeObject<List<UsuarioEtapaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _Flujo;
            }
        }

    }
}
