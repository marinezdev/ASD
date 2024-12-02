using ASD.Areas.Inventario.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_EquipoRutinaService : ICat_EquipoRutinaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EquipoRutinaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<Cat_EquipoRutinaViewModel> CreateCat_EquipoRutina(Cat_EquipoRutinaViewModel cat_EquipoRutina)
        {
            Cat_EquipoRutinaViewModel? _result = new Cat_EquipoRutinaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_EquipoRutina_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEquipo", cat_EquipoRutina.Equipo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_EquipoRutina.Nombre);
                cmd.Parameters.AddWithValue("Observaciones", cat_EquipoRutina.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EquipoRutinaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
