using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_EstatusFechaAtencionService: ICat_EstatusFechaAtencionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EstatusFechaAtencionService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_EstatusFechaAtencionViewModel>> GetCat_EstatusFechaAtencion()
        {
            List<Cat_EstatusFechaAtencionViewModel>? _result = new List<Cat_EstatusFechaAtencionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusFechaAtencion_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_EstatusFechaAtencionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstatusFechaAtencionViewModel> InsertEstatusFechaAtencion(Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion)
        {
            Cat_EstatusFechaAtencionViewModel _result = new Cat_EstatusFechaAtencionViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusFechaAtencion_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", cat_EstatusFechaAtencion.Nombre);
                cmd.Parameters.AddWithValue("Color", cat_EstatusFechaAtencion.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_EstatusFechaAtencionViewModel> UpdateEstatusFechaAtencion(Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion)
        {
            Cat_EstatusFechaAtencionViewModel _result = new Cat_EstatusFechaAtencionViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusFechaAtencion_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_EstatusFechaAtencion.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_EstatusFechaAtencion.Nombre);
                cmd.Parameters.AddWithValue("Color", cat_EstatusFechaAtencion.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_EstatusFechaAtencionViewModel> DeleteEstatusFechaAtencion(Cat_EstatusFechaAtencionViewModel cat_EstatusFechaAtencion)
        {
            Cat_EstatusFechaAtencionViewModel _result = new Cat_EstatusFechaAtencionViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusFechaAtencion_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_EstatusFechaAtencion.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusFechaAtencionViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
