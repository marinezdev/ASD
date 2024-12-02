using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_EstatusOrdenTrabajoService : ICat_EstatusOrdenTrabajoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EstatusOrdenTrabajoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<Cat_EstatusOrdenTrabajoViewModel>> GetCat_EstatusOrdenTrabajo_Seleccionar()
        {
            List<Cat_EstatusOrdenTrabajoViewModel>? _result = new List<Cat_EstatusOrdenTrabajoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusOrdenTrabajo_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_EstatusOrdenTrabajoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstatusOrdenTrabajoViewModel> InsertEstatusOrdenTrabajo(Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo) {
            Cat_EstatusOrdenTrabajoViewModel _result = new Cat_EstatusOrdenTrabajoViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusOrdenTrabajo_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", cat_EstatusOrdenTrabajo.Nombre);
                cmd.Parameters.AddWithValue("Color", cat_EstatusOrdenTrabajo.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusOrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_EstatusOrdenTrabajoViewModel> UpdateEstatusOrdenTrabajo(Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo) {
            Cat_EstatusOrdenTrabajoViewModel _result = new Cat_EstatusOrdenTrabajoViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusOrdenTrabajo_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_EstatusOrdenTrabajo.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_EstatusOrdenTrabajo.Nombre);
                cmd.Parameters.AddWithValue("Color", cat_EstatusOrdenTrabajo.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusOrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_EstatusOrdenTrabajoViewModel> DeleteEstatusOrdenTrabajo(Cat_EstatusOrdenTrabajoViewModel cat_EstatusOrdenTrabajo) {
            Cat_EstatusOrdenTrabajoViewModel _result = new Cat_EstatusOrdenTrabajoViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusOrdenTrabajo_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_EstatusOrdenTrabajo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusOrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
