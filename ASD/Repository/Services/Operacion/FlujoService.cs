using ASD.Areas.Administracion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Operacion.Models;
using ASD.Repository.Interface.Operacion;

namespace ASD.Repository.Services.Operacion
{
    public class FlujoService : IFlujoRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public FlujoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<FlujoViewModel>> GetFlujo_Seleccionar()
        {
            List<FlujoViewModel> _Flujo = new List<FlujoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Flujo_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _Flujo = JsonConvert.DeserializeObject<List<FlujoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _Flujo;
            }
        }

        public async Task<List<FlujoViewModel>> GetCat_Flujo_Listar_IdUsuario(FlujoViewModel Flujo)
        {
            List<FlujoViewModel> _Flujo = new List<FlujoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Flujo_Listar_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Flujo.Id);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _Flujo = JsonConvert.DeserializeObject<List<FlujoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _Flujo;
            }
        }


        public async Task<List<FlujoViewModel>> GetCat_Flujo_Listar_Faltante (UsuarioFlujoViewModel Flujo)
        {
            List<FlujoViewModel> _Flujo = new List<FlujoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Flujo_Listar_Faltante", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Flujo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _Flujo = JsonConvert.DeserializeObject<List<FlujoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _Flujo;
            }
        }



        public async Task<UsuarioFlujoViewModel> Add_UsuarioFlujo(UsuarioFlujoViewModel UsuarioFlujo)
        {
            UsuarioFlujoViewModel _result = new UsuarioFlujoViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.UsuarioFlujo_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", UsuarioFlujo.Usuario.Id);
                cmd.Parameters.AddWithValue("IdFlujo", UsuarioFlujo.Flujo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<UsuarioFlujoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}

