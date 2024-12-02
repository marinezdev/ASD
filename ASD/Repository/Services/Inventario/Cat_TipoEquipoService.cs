using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;
using ASD.Repository.Interface.Inventario;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ASD.Repository.Services.Inventario
{
    public class Cat_TipoEquipoService : ICat_TipoEquipoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_TipoEquipoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_TipoEquipoViewModel>> GetCat_TipoEquipo_Seleccionar()
        {
            List<Cat_TipoEquipoViewModel>? _result = new List<Cat_TipoEquipoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_TipoEquipo_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_TipoEquipoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_TipoEquipoViewModel> CreateCat_TipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel? _result = new Cat_TipoEquipoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_TipoEquipo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", cat_TipoEquipo.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoEquipoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_TipoEquipoViewModel> Delete_TipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel _result = new Cat_TipoEquipoViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_TipoEquipo_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoEquipoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<Cat_TipoEquipoViewModel> Update_TipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            Cat_TipoEquipoViewModel _result = new Cat_TipoEquipoViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_TipoEquipo_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEquipo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_TipoEquipo.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoEquipoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
