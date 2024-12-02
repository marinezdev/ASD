using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class TicketEquipoImagenService : ITicketEquipoImagenRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketEquipoImagenService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<TicketEquipoImagenViewModel>> GetTicketEquipoImagen_Seleccionar_IdTicketEquipo(TicketEquipoViewModel ticketEquipo)
        {
            List<TicketEquipoImagenViewModel>? _result = new List<TicketEquipoImagenViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipoImagen_Seleccionar_IdTicketEquipo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicketEquipo", ticketEquipo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketEquipoImagenViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketEquipoImagenViewModel> UpdateTicketEquipoImagen_Actualizar(TicketEquipoImagenViewModel ticketEquipoImagen)
        {
            TicketEquipoImagenViewModel? _result = new TicketEquipoImagenViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipoImagen_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticketEquipoImagen.Id);
                cmd.Parameters.AddWithValue("Imagen", ticketEquipoImagen.Imagen);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEquipoImagenViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketEquipoImagenViewModel> UpdateTicketEquipoImagen_Actualizar_ImagenVS(TicketEquipoImagenViewModel ticketEquipoImagen)
        {
            TicketEquipoImagenViewModel? _result = new TicketEquipoImagenViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipoImagen_Actualizar_ImagenVS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticketEquipoImagen.Id);
                cmd.Parameters.AddWithValue("Imagen", ticketEquipoImagen.Imagen);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEquipoImagenViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketEquipoImagenViewModel> GetTicketEquipoImagen_Seleccionar_Id(TicketEquipoImagenViewModel ticketEquipoImagen)
        {
            TicketEquipoImagenViewModel? _result = new TicketEquipoImagenViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketEquipoImagen_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticketEquipoImagen.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketEquipoImagenViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
