using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Operacion;
using ASD.Repository.Interface.Empresa;
using System.Net.Sockets;

namespace ASD.Repository.Services.Empresa
{
    public class SucursalService : ISucursalRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public SucursalService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<SucursalViewModel>> GetSucursal_Seleccionar()
        {
            List<SucursalViewModel>? _result = new List<SucursalViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Sucursal_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<SucursalViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<SucursalViewModel> GetSucursal_Seleccionar_Id(SucursalViewModel sucursal)
        {
            SucursalViewModel? _result = new SucursalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Sucursal_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", sucursal.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SucursalViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<SucursalViewModel> CreateSucursal(SucursalViewModel sucursal)
        {
            SucursalViewModel? _result = new SucursalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Sucursal_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEmpresa", sucursal.Empresa.Id);
                cmd.Parameters.AddWithValue("IdColonia", sucursal.Cat_Colonia.Id);
                cmd.Parameters.AddWithValue("IdTipo", sucursal.Cat_TipoSucursal.Id);
                cmd.Parameters.AddWithValue("Nombre", sucursal.Nombre);
                cmd.Parameters.AddWithValue("Direccion", sucursal.Direccion);
                cmd.Parameters.AddWithValue("Latitud", sucursal.Latitud);
                cmd.Parameters.AddWithValue("Longitud", sucursal.Longitud);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SucursalViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<List<SucursalViewModel>> GetSucursalIdEmpresa(SucursalViewModel sucursal)
        {
            List<SucursalViewModel> ? _result = new List<SucursalViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Sucursal_listar_IdEmpresa", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", sucursal.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject <List<SucursalViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
