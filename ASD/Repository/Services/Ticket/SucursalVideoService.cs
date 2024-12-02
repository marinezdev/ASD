using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class SucursalVideoService : ISucursalVideoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public SucursalVideoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<SucursalVideoViewModel> CreateSucursalVideo(SucursalVideoViewModel sucursaVideo)
        {
            SucursalVideoViewModel? _result = new SucursalVideoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.SucursalVideo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", sucursaVideo.Ticket.Id);
                cmd.Parameters.AddWithValue("IdCatVideo", sucursaVideo.Cat_SucursalVideo.Id);
                cmd.Parameters.AddWithValue("IdUsuario", sucursaVideo.Usuario.Id);
                cmd.Parameters.AddWithValue("NmOriginal", sucursaVideo.NmOriginal);
                cmd.Parameters.AddWithValue("NmArchivo", sucursaVideo.NmArchivo);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SucursalVideoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<SucursalVideoViewModel> GetSucursalVideo_Seleccionar_Id(SucursalVideoViewModel sucursalVideo)
        {
            SucursalVideoViewModel? _result = new SucursalVideoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.SucursalVideo_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", sucursalVideo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SucursalVideoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<SucursalVideoViewModel>> GetSucursalVideo_Seleccionar_IdTikcet(TicketViewModel ticket)
        {
            List<SucursalVideoViewModel>? _result = new List<SucursalVideoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.SucursalVideo_Seleccionar_IdTikcet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<SucursalVideoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
