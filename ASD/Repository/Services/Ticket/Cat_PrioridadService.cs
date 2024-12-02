using ASD.Areas.Administracion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_PrioridadService : ICat_PrioridadRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_PrioridadService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<Cat_PrioridadViewModel>> GetCat_Prioridad_Seleccionar()
        {
            List<Cat_PrioridadViewModel>? _result = new List<Cat_PrioridadViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_Prioridad_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_PrioridadViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<Cat_PrioridadViewModel> InsertPrioridad(Cat_PrioridadViewModel cat_Prioridad) {
            Cat_PrioridadViewModel _result = new Cat_PrioridadViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_Prioridad_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", cat_Prioridad.Nombre);
                cmd.Parameters.AddWithValue("Color", cat_Prioridad.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_PrioridadViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_PrioridadViewModel> UpdatePrioridad(Cat_PrioridadViewModel cat_Prioridad)
        {
            Cat_PrioridadViewModel _result = new Cat_PrioridadViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_Prioridad_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_Prioridad.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_Prioridad.Nombre);
                cmd.Parameters.AddWithValue("Color", cat_Prioridad.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_PrioridadViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_PrioridadViewModel> DeletePrioridad(Cat_PrioridadViewModel cat_Prioridad)
        {
            Cat_PrioridadViewModel _result = new Cat_PrioridadViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_Prioridad_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_Prioridad.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_PrioridadViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
