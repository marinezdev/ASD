using ASD.Areas.Empresa.Models;
using ASD.Areas.Inventario.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Inventario;

namespace ASD.Repository.Services.Inventario
{
    public class Cat_CategoriaService : ICat_CategoriaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_CategoriaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_CategoriaViewModel>> GetCat_Categoria_Seleccionar_IdTipoEquipo(Cat_TipoEquipoViewModel cat_TipoEquipo)
        {
            List<Cat_CategoriaViewModel>? _result = new List<Cat_CategoriaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Categoria_Seleccionar_IdTipoEquipo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTipoEquipo", cat_TipoEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_CategoriaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_CategoriaViewModel> CreateCat_Categoria_Crear(Cat_CategoriaViewModel cat_Categoria)
        {
            Cat_CategoriaViewModel? _result = new Cat_CategoriaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Categoria_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTipoEquipo", cat_Categoria.Cat_TipoEquipo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_Categoria.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_CategoriaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }




        public async Task<Cat_CategoriaViewModel> Delete_Categoria(Cat_CategoriaViewModel cat_TipoEquipo)
        {
            Cat_CategoriaViewModel _result = new Cat_CategoriaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Categoria_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_CategoriaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<Cat_CategoriaViewModel> Update_Categoria(Cat_CategoriaViewModel cat_TipoEquipo)
        {
            Cat_CategoriaViewModel _result = new Cat_CategoriaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_Categoria_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEquipo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_TipoEquipo.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_CategoriaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
