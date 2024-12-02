using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;

namespace ASD.Repository.Services.Empresa
{
    public class CuadrillaService : ICuadrillaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public CuadrillaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<CuadrillaViewModel>> GetCuadrilla_Seleccionar()
        {
            List<CuadrillaViewModel>? _result = new List<CuadrillaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cuadrilla_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CuadrillaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<CuadrillaViewModel>> GetCuadrillaIdEmpresa(CuadrillaViewModel Cuadrilla)
        {
            List<CuadrillaViewModel>? _result = new List<CuadrillaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cuadrilla_listar_IdEmpresa", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cuadrilla.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CuadrillaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<CuadrillaViewModel> CreateCuadrilla(CuadrillaViewModel Cuadrilla)
        {
            CuadrillaViewModel _result = new CuadrillaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cuadrilla_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEmpresa", Cuadrilla.Empresa.Id);
                cmd.Parameters.AddWithValue("Nombre", Cuadrilla.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
            
        }

            public async Task<CuadrillaViewModel> Delete_Cuadrilla(CuadrillaViewModel Cuadrilla)
        {
            CuadrillaViewModel _result = new CuadrillaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Cuadrilla_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cuadrilla.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CuadrillaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

    }
}
