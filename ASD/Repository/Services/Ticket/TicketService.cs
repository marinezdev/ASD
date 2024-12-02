using ASD.Areas.Administracion.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Ticket.Models.Consulta;
using ASD.Areas.Ticket.Models;
using ASD.Repository.Services.Empresa;
using ASD.Areas.Empresa.Models;
using ASD.Areas.TicketUnitario.Models;

namespace ASD.Repository.Services.Ticket
{
    public class TicketService : ITicketRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public TicketService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }
        public async Task<List<CTicketViewModel>> GetTicket_Seleccionar_IdUsuario(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Seleccionar_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Selecionar_EnOperacion(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_EnOperacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Selecionar_OperacionOrdenTrabajo(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_OperacionOrdenTrabajo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Selecionar_ValidadoCliente(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_ValidadoCliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Selecionar_ValidadoSupervisor(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_ValidadoSupervisor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
		}
		public async Task<List<CTicketViewModel>> GetTicket_Selecionar_AtendidosOperador(UsuarioViewModel usuario)
		{
			List<CTicketViewModel>? _result = new List<CTicketViewModel>();

			using (var conexion = new SqlConnection(_cadenaSQL))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_AtendidosOperador", conexion);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

				using (var dr = await cmd.ExecuteReaderAsync())
				{
					if (await dr.ReadAsync())
					{
						_result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
					}
				}
				conexion.Close();
				return _result;
			}
        }
        public async Task<List<CTicketViewModel>> GetTicket_Selecionar_EnReasignacion(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_EnReasignacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Tickets_Seleccionar_Estado(UsuarioViewModel usuario, Cat_EstadoViewModel cat_Estado)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Tickets_Seleccionar_Estado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);
                cmd.Parameters.AddWithValue("IdEstado", cat_Estado.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<CTicketViewModel> GetTicket_Seleccionar_Id(TicketViewModel ticket)
        {
            CTicketViewModel? _result = new CTicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<CreateTicketViewModel> CreateTicket(CreateTicketViewModel ticket)
        {
            CreateTicketViewModel? _result = new CreateTicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", ticket.Ticket.Flujo.Id);
                cmd.Parameters.AddWithValue("IdPrioridad", ticket.Ticket.Cat_Prioridad.Id);
                cmd.Parameters.AddWithValue("IdSucursal", ticket.Ticket.Sucursal.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("FechaAtencion", ticket.FechaAtencion.FechaAtencion);
                cmd.Parameters.AddWithValue("Titulo", ticket.Ticket.Titulo);
                cmd.Parameters.AddWithValue("Detalle", ticket.Ticket.Detalle);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CreateTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<CreateTicketViewModel> CreateTicket_Crear_Usuario(CreateTicketViewModel ticket)
        {
            CreateTicketViewModel? _result = new CreateTicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Crear_Usuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdFlujo", ticket.Ticket.Flujo.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Titulo", ticket.Ticket.Titulo);
                cmd.Parameters.AddWithValue("Detalle", ticket.Ticket.Detalle);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CreateTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Seleccionar_IdEstatus(TicketViewModel ticket)
        {
            List <CTicketViewModel> ? _result = new  List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Seleccionar_IdEstatus", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject <List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketViewModel> UpdateTicket_ActualizarEstatus(TicketViewModel ticket)
        {
            TicketViewModel? _result = new TicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_ActualizarEstatus", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("IdEstatus", ticket.Cat_EstatusTicket.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticket.Detalle);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicket_Selecionar_EnOperacion_Usuario(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_EnOperacion_Usuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketViewModel> UpdateTicket_Procesar_SupervisorAsae(TicketViewModel ticket)
        {
            TicketViewModel? _result = new TicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Procesar_SupervisorAsae", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticket.Detalle);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketViewModel> UpdateTicket_Procesar_SupervisorCliente(TicketViewModel ticket)
        {
            TicketViewModel? _result = new TicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Procesar_SupervisorCliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticket.Detalle);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketViewModel> UpdateTicketProcesar_Cliente(TicketViewModel ticket)
        {
            TicketViewModel? _result = new TicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketProcesar_Cliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticket.Detalle);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketViewModel> UpdateTicket(TicketViewModel ticket)
        {
            TicketViewModel? _result = new TicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("IdPrioridad", ticket.Cat_Prioridad.Id);
                cmd.Parameters.AddWithValue("Titulo", ticket.Titulo);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketViewModel> UpdateTicket_Actualizar_IdPrioridad(TicketViewModel ticket)
        {
            TicketViewModel? _result = new TicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Actualizar_IdPrioridad", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("IdPrioridad", ticket.Cat_Prioridad.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<CTicketViewModel>> GetTicketUnitario_Selecionar_EnOperacion(UsuarioViewModel usuario)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketUnitario_Selecionar_EnOperacion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketAtenderViewModel> Ticket_Procesar_Atendido(TicketAtenderViewModel ticket)
        {
            TicketAtenderViewModel? _result = new TicketAtenderViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Procesar_Atendido", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Observaciones", ticket.Observaciones);
                cmd.Parameters.AddWithValue("FechaTermino", ticket.FechaTermino);
                cmd.Parameters.AddWithValue("FechaInicio", ticket.FechaInicio);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketAtenderViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<CTicketViewModel> GetTicket_Seleccionar_Id(TicketAtenderViewModel ticket)
        {
            CTicketViewModel? _result = new CTicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<CTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketAtenderViewModel> Ticket_Procesar_Cierre(TicketAtenderViewModel ticket)
        {
            TicketAtenderViewModel? _result = new TicketAtenderViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Procesar_Cierre", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketAtenderViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketAtenderViewModel> Ticket_Procesar_NoAtendido(TicketAtenderViewModel ticket)
        {
            TicketAtenderViewModel? _result = new TicketAtenderViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Procesar_NoAtendido", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketAtenderViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTicketsAnio()
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTicketsAnio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTicketsPorMesYAnio()
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTicketsPorMesYAnio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTotalTicketsPorEstatusAnio(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTotalTicketsPorEstatusAnio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTotalTicketsPorFlujoAnio(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTotalTicketsPorFlujoAnio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTicketsAtendidos(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTicketsAtendidos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerResumenFaltante(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerResumenFaltante", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTicketIdflujoUser(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTicketIdflujoUser", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);
                cmd.Parameters.AddWithValue("IdUsuario", Model.IdUsuario);
                cmd.Parameters.AddWithValue("IdFlujo", Model.IdFlujo);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> GetTicketEstatusAnio(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.GetTicketEstatusAnio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);
                cmd.Parameters.AddWithValue("IdEstatus", Model.IdFlujo);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> GetTicketAnioMonth(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.GetTicketAnioMonth", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);
                cmd.Parameters.AddWithValue("Mes", Model.Mes);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTicketXservicio(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTicketXservicio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);
                cmd.Parameters.AddWithValue("Mes", Model.Mes);
                cmd.Parameters.AddWithValue("IdFlujo", Model.IdFlujo);



                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<TicketReporteViewmodel>> ObtenerTicketFinalizado(TicketReporteViewmodel Model)
        {
            List<TicketReporteViewmodel>? _result = new List<TicketReporteViewmodel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.ObtenerTicketFinalizado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);
                cmd.Parameters.AddWithValue("IdUsuario", Model.IdUsuario);
                cmd.Parameters.AddWithValue("IdFlujo", Model.IdFlujo);



                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<TicketReporteViewmodel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<TicketReporteViewmodel> TicketFechaCorte(TicketReporteViewmodel Model)
        {
            TicketReporteViewmodel? _result = new TicketReporteViewmodel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.TicketFechaCorte", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Anio", Model.Anio);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<TicketReporteViewmodel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }



        public async Task<List<CTicketViewModel>> Ticket_Selecionar_EnOperacionAdmin()
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Selecionar_EnOperacionAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<List<CTicketViewModel>> GetTicket_Tickets_Seleccionar_Estado(Cat_EstadoViewModel cat_Estado)
        {
            List<CTicketViewModel>? _result = new List<CTicketViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Ticket_Tickets_Seleccionar_EstadoAdmin", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdEstado", cat_Estado.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<CTicketViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}





    