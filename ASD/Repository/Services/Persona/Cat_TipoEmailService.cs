using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Persona;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Persona.Models;

namespace ASD.Repository.Services.Persona
{
    public class Cat_TipoEmailService : ICat_TipoEmailRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_TipoEmailService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_TipoEmailViewModel>> GetCat_TipoEmail_Seleccionar()
        {
            List<Cat_TipoEmailViewModel>? _result = new List<Cat_TipoEmailViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Persona.Cat_TipoEmail_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_TipoEmailViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_TipoEmailViewModel> CreateCat_TipoEmail(Cat_TipoEmailViewModel Cat_TipoEmail)
        {
            Cat_TipoEmailViewModel? _result = new Cat_TipoEmailViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Persona.Cat_TipoEmail_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", Cat_TipoEmail.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoEmailViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_TipoEmailViewModel> Delete_TipoEmail(Cat_TipoEmailViewModel cat_TipoEmail)
        {
            Cat_TipoEmailViewModel _result = new Cat_TipoEmailViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Persona.Cat_TipoEmail_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEmail.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoEmailViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<Cat_TipoEmailViewModel> Update_TipoEmail(Cat_TipoEmailViewModel cat_TipoEmail)
        {
            Cat_TipoEmailViewModel _result = new Cat_TipoEmailViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Persona.Cat_TipoEmail_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_TipoEmail.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_TipoEmail.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_TipoEmailViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }
    }
}
