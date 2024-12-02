using System;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.Ticket;
using Newtonsoft.Json;

namespace ASD.Repository.Services.Ticket
{
	public class TicketFechaAtencionService : ITicketFechaAtencionRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketFechaAtencionService(IConfiguration configuration)
		{ 
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<CTicketFechaAtencionViewModel>> Ticket_FechaAtencion_Seleccionar_IdUser(CTicketFechaAtencionViewModel ticket)
        {
            List<CTicketFechaAtencionViewModel>? _result = new List<CTicketFechaAtencionViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FechaAtencion_Seleccionar_IdUser", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketFechaAtencionViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}

