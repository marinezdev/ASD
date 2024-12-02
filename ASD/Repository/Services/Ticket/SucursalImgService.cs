using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class SucursalImgService : ISucursalImgRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public SucursalImgService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<SucursalImgViewModel> CreateSucursalImg(SucursalImgViewModel sucursalImg)
        {
            SucursalImgViewModel? _result = new SucursalImgViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.SucursalImg_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", sucursalImg.Ticket.Id);
                cmd.Parameters.AddWithValue("IdCatImagen", sucursalImg.Cat_SucursalImagen.Id);
                cmd.Parameters.AddWithValue("IdUsuario", sucursalImg.Usuario.Id);
                cmd.Parameters.AddWithValue("NmOriginal", sucursalImg.NmOriginal);
                cmd.Parameters.AddWithValue("NmArchivo", sucursalImg.NmArchivo);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SucursalImgViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<SucursalImgViewModel> GetSucursalImg_Seleccionar_Id(SucursalImgViewModel sucursalImg)
        {
            SucursalImgViewModel? _result = new SucursalImgViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.SucursalImg_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", sucursalImg.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SucursalImgViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<SucursalImgViewModel>> GetSucursalImg_Seleccionar_IdTikcet(TicketViewModel ticket)
        {
            List<SucursalImgViewModel>? _result = new List<SucursalImgViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.SucursalImg_Seleccionar_IdTikcet", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<SucursalImgViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
