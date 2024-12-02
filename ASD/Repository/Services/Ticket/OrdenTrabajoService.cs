using ASD.Areas.Operacion.Models;
using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;

namespace ASD.Repository.Services.Ticket
{
    public class OrdenTrabajoService : IOrdenTrabajoRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public OrdenTrabajoService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        
        public async Task<OrdenTrabajoViewModel> CreateOrdenTrabajo(OrdenTrabajoViewModel ordenTrabajo)
        {
            OrdenTrabajoViewModel? _result = new OrdenTrabajoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.OrdenTrabajo_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ordenTrabajo.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ordenTrabajo.Usuario.Id);
                cmd.Parameters.AddWithValue("NmOriginal", ordenTrabajo.NmOriginal);
                cmd.Parameters.AddWithValue("NmArchivo", ordenTrabajo.NmArchivo);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<OrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<OrdenTrabajoViewModel> GetOrdenTrabajo_Seleccionar_IdTicket(TicketViewModel ticket)
        {
            OrdenTrabajoViewModel? _result = new OrdenTrabajoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.OrdenTrabajo_Seleccionar_IdTicket", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<OrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<OrdenTrabajoViewModel> UpdateOrdenTrabajo_ActualizarEstatus(OrdenTrabajoViewModel ordenTrabajo)
        {
            OrdenTrabajoViewModel? _result = new OrdenTrabajoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.OrdenTrabajo_ActualizarEstatus", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ordenTrabajo.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ordenTrabajo.Usuario.Id);
                cmd.Parameters.AddWithValue("IdEstatus", ordenTrabajo.Cat_EstatusOrdenTrabajo.Id);
                cmd.Parameters.AddWithValue("Observaciones", ordenTrabajo.Observaciones);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<OrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<OrdenTrabajoViewModel> CreateOrdenTrabajo_ProcesarEtapa(OrdenTrabajoViewModel ordenTrabajo)
        {
            OrdenTrabajoViewModel? _result = new OrdenTrabajoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.OrdenTrabajo_ProcesarEtapa", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ordenTrabajo.Ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ordenTrabajo.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<OrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<OrdenTrabajoViewModel> Archivo_Seleccionar_Id(ArchivoViewModel archivo)
        {
            OrdenTrabajoViewModel? _result = new OrdenTrabajoViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Archivo_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", archivo.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<OrdenTrabajoViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
