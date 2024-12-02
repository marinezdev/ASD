using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Persona.Models;
using ASD.Areas.Administracion.Models;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_EstatusTicketService : ICat_EstatusTicketRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EstatusTicketService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<Cat_EstatusTicketViewModel>> Cat_EstatusTicket_Seleccionar()
        {
            List<Cat_EstatusTicketViewModel>? _result = new List<Cat_EstatusTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusTicket_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_EstatusTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<CountTicketViewModel>> Cat_EstatusTicket_SeleccionarConteo(UsuarioViewModel usuario)
        {
            List<CountTicketViewModel>? _result = new List<CountTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusTicket_SeleccionarConteo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CountTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstatusTicketViewModel> DeleteStatusTicket(Cat_EstatusTicketViewModel Cat_EstatusTicket)
        {
            Cat_EstatusTicketViewModel _result = new Cat_EstatusTicketViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusTicket_Eliminar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cat_EstatusTicket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<Cat_EstatusTicketViewModel> UpdateStatusTicket(Cat_EstatusTicketViewModel Cat_EstatusTicket)
        {
            Cat_EstatusTicketViewModel _result = new Cat_EstatusTicketViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusTicket_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cat_EstatusTicket.Id);
                cmd.Parameters.AddWithValue("Nombre", Cat_EstatusTicket.Nombre);
                cmd.Parameters.AddWithValue("Color", Cat_EstatusTicket.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<Cat_EstatusTicketViewModel> InsertStatusTicket(Cat_EstatusTicketViewModel Cat_EstatusTicket)
        {
            Cat_EstatusTicketViewModel _result = new Cat_EstatusTicketViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusTicket_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", Cat_EstatusTicket.Nombre);
                cmd.Parameters.AddWithValue("Color", Cat_EstatusTicket.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }



        public async Task<List<CountTicketViewModel>> Cat_EstatusTicket_SeleccionarConteoAdmin()
        {
            List<CountTicketViewModel>? _result = new List<CountTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_EstatusTicket_SeleccionarConteoAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CountTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
