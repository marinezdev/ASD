using ASD.Areas.Administracion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Inventario;

namespace ASD.Repository.Services.Inventario
{
    public class EquipoService : IEquipoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public EquipoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<EquipoViewModel>> GetEquipo_Seleccionar_IdSucursal(SucursalViewModel sucursal)
        {
            List<EquipoViewModel>? _result = new List<EquipoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Equipo_Seleccionar_IdSucursal", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdSucursal", sucursal.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EquipoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<EquipoViewModel> CreateEquipo(EquipoViewModel equipo)
        {
            EquipoViewModel? _result = new EquipoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Equipo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdSucursal", equipo.Sucursal.Id);
                cmd.Parameters.AddWithValue("IdModelo", equipo.Cat_Modelo.Id);
                cmd.Parameters.AddWithValue("IdEstatus", equipo.Cat_EstatusEquipo.Id);
                cmd.Parameters.AddWithValue("Serie", equipo.Serie);
                cmd.Parameters.AddWithValue("FechaApertura", equipo.FechaApertura);
                cmd.Parameters.AddWithValue("FechaAdquisicion", equipo.FechaAdquisicion);
                cmd.Parameters.AddWithValue("FechaCaducidad", equipo.FechaCaducidad);
                cmd.Parameters.AddWithValue("FechaGarantia", equipo.FechaGarantia);
                cmd.Parameters.AddWithValue("Observaciones", equipo.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EquipoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
