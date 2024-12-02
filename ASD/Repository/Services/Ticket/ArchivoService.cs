using ASD.Areas.Ticket.Models.Consulta;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class ArchivoService : IArchivoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public ArchivoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<ArchivoViewModel> CreateArchivo(ArchivoViewModel archivo)
        {
            ArchivoViewModel? _result = new ArchivoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Archivo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", archivo.Usuario.Id);
                cmd.Parameters.AddWithValue("IdTicket", archivo.Ticket.Id);
                cmd.Parameters.AddWithValue("NmOriginal", archivo.NmOriginal);
                cmd.Parameters.AddWithValue("NmArchivo", archivo.NmArchivo);
                cmd.Parameters.AddWithValue("Observaciones", archivo.Observaciones);
                cmd.Parameters.AddWithValue("Extencion", archivo.Extencion);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<ArchivoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<ArchivoViewModel>> GetArchivo_Seleccionar_IdTicket(TicketViewModel ticket)
        {
            List<ArchivoViewModel>? _result = new List<ArchivoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Archivo_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<ArchivoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<ArchivoViewModel> DeleteArchivo(ArchivoViewModel archivo)
        {
            ArchivoViewModel? _result = new ArchivoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Archivo_Eliminar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", archivo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<ArchivoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
