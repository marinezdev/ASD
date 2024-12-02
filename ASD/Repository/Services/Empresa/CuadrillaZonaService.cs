using ASD.Areas.Empresa.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Empresa;
using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Services.Empresa
{
    public class CuadrillaZonaService : ICuadrillaZonaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public CuadrillaZonaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }


        public async Task<List<CuadrillaZonaViewModel>> Get_Cuadrilla(CuadrillaZonaViewModel CuadrillaZona)
        {
            List<CuadrillaZonaViewModel>? _result = new List<CuadrillaZonaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.CuadrillaZona_SeleccionarIdCuadrilla", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", CuadrillaZona.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CuadrillaZonaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<CuadrillaZonaViewModel> Delete_CuadrillaZona(CuadrillaZonaViewModel CuadrillaZona)
        {
            CuadrillaZonaViewModel _result = new CuadrillaZonaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.CuadrillaZona_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", CuadrillaZona.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CuadrillaZonaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
        public async Task<CuadrillaZonaViewModel> Add_CuadrillaZona(CuadrillaZonaViewModel CuadrillaZona)
        {
            CuadrillaZonaViewModel _result = new CuadrillaZonaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.CuadrillaZona_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdCuadrilla", CuadrillaZona.Cuadrilla.Id);
                cmd.Parameters.AddWithValue("IdColonia", CuadrillaZona.Colonia.Id);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CuadrillaZonaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
