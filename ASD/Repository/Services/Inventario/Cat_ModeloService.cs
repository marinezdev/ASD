using ASD.Areas.Inventario.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Inventario;

namespace ASD.Repository.Services.Inventario
{
    public class Cat_ModeloService : ICat_ModeloRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_ModeloService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_ModeloViewModel>> GetCat_Modelo_Seleccionar_IdCategoria(Cat_CategoriaViewModel cat_Categoria)
        {
            List<Cat_ModeloViewModel>? _result = new List<Cat_ModeloViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Modelo_Seleccionar_IdCategoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdCategoria", cat_Categoria.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_ModeloViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_ModeloViewModel> CreateCat_Modelo_Crear(Cat_ModeloViewModel cat_Modelo)
        {
            Cat_ModeloViewModel? _result = new Cat_ModeloViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Modelo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdCategoria", cat_Modelo.Cat_Categoria.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_Modelo.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_ModeloViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<Cat_ModeloViewModel> Delete_Modelo(Cat_ModeloViewModel cat_TipoEquipo)
        {
            Cat_ModeloViewModel _result = new Cat_ModeloViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Modelo_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_ModeloViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<Cat_ModeloViewModel> Update_Modelo(Cat_ModeloViewModel cat_TipoEquipo)
        {
            Cat_ModeloViewModel _result = new Cat_ModeloViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Modelo_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEquipo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_TipoEquipo.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_ModeloViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
