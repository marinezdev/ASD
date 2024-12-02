using ASD.Areas.Administracion.Models;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.TicketAtencionMovil;
using Newtonsoft.Json;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Areas.Ticket.Models.Consulta;
using System.Text;
using System.Buffers.Text;
using System.Security.Cryptography;

namespace ASD.Repository.Services.TicketAtencionMovil
{
    public class CierreClienteService:ICierreClienteRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public CierreClienteService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }


        public async Task<CierreClienteViewModel> Create_CierreCliente(CierreClienteViewModel CierreCliente)
        {
            CierreClienteViewModel _result = new CierreClienteViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaRecibido_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", CierreCliente.IdTicket);
                cmd.Parameters.AddWithValue("Pregunta1", CierreCliente.Pregunta1);
                cmd.Parameters.AddWithValue("Pregunta2", CierreCliente.Pregunta2);
                cmd.Parameters.AddWithValue("Pregunta3", CierreCliente.Pregunta3);
                cmd.Parameters.AddWithValue("Nombre", CierreCliente.Nombre);
                cmd.Parameters.AddWithValue("Firma", CierreCliente.Firma);
                cmd.Parameters.AddWithValue("Observaciones", CierreCliente.Observaciones);

                try
                {
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            _result = JsonConvert.DeserializeObject<CierreClienteViewModel>(dr.GetValue(0).ToString());
                        }
                    }
                    conexion.Close();

                } catch (Exception ex) {
                }

                return _result;

            }
        }


        public async Task<CierreClienteViewModel> Get_CierreCliente(CierreClienteViewModel CierreCliente)
        {
            CierreClienteViewModel? _result = new CierreClienteViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaRecibido_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", CierreCliente.IdTicket);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                
                            _result = JsonConvert.DeserializeObject<CierreClienteViewModel>(dr.GetValue(0).ToString());
                      
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<CierreClienteViewModel> Delete_CierreClienteFirma(CierreClienteViewModel CierreCliente)
        {
            CierreClienteViewModel? _result = new CierreClienteViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaRecibido_EliminarFirma", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", CierreCliente.IdTicket);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {

                        _result = JsonConvert.DeserializeObject<CierreClienteViewModel>(dr.GetValue(0).ToString());

                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
