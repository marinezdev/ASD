using ASD.Areas.Ticket.Models.Consulta;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Inventario.Models;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_EquipoImagenService : ICat_EquipoImagenRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EquipoImagenService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<Cat_EquipoImagenViewModel> CreateCat_EquipoImagen(Cat_EquipoImagenViewModel cat_EquipoImagen)
        {
            Cat_EquipoImagenViewModel? _result = new Cat_EquipoImagenViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Cat_EquipoImagen_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEquipo", cat_EquipoImagen.Equipo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_EquipoImagen.Nombre);
                cmd.Parameters.AddWithValue("Observaciones", cat_EquipoImagen.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EquipoImagenViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
