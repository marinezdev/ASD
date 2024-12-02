using ASD.Areas.Administracion.Models;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.Administracion;
using Newtonsoft.Json;
using ASD.Areas.TicketAtencionMovil.Models;
using ASD.Areas.Ticket.Models;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Services.Administracion
{
    public class FirmaElectronicaService: IFirmaElectronicaRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public FirmaElectronicaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }


        public async Task<FirmaElectronicaViewModel> CreateFirma(FirmaElectronicaViewModel firmaElectronica)
        {
            FirmaElectronicaViewModel _result = new FirmaElectronicaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Firma_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", firmaElectronica.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<FirmaElectronicaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        public async Task<InformacionAdicionalViewModel> Create_Signature(InformacionAdicionalViewModel InformacionAdicional)
        {
            InformacionAdicionalViewModel _result = new InformacionAdicionalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaCuadrilla_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", InformacionAdicional.Usuario.Id);
                cmd.Parameters.AddWithValue("IdTicket", InformacionAdicional.IdTicket);
                cmd.Parameters.AddWithValue("Firma", InformacionAdicional.Firma);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<InformacionAdicionalViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<InformacionAdicionalViewModel> GetSignature(TicketViewModel ticket)
        {
            InformacionAdicionalViewModel? _result = new InformacionAdicionalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaCuadrilla_Seleccionar_Id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", ticket.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {

                        _result = JsonConvert.DeserializeObject<InformacionAdicionalViewModel>(dr.GetValue(0).ToString());

                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<InformacionAdicionalViewModel> Delete_Firma(InformacionAdicionalViewModel InformacionAdicional)
        {
            InformacionAdicionalViewModel? _result = new InformacionAdicionalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaCuadrilla_EliminarFirma", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", InformacionAdicional.IdTicket);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {

                        _result = JsonConvert.DeserializeObject<InformacionAdicionalViewModel>(dr.GetValue(0).ToString());

                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<InformacionAdicionalViewModel> UpdateIDC_Firma(InformacionAdicionalViewModel InformacionAdicional)
        {
            InformacionAdicionalViewModel? _result = new InformacionAdicionalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaCuadrilla_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", InformacionAdicional.Id);
                cmd.Parameters.AddWithValue("IdTicket", InformacionAdicional.IdTicket);
                cmd.Parameters.AddWithValue("Firma", InformacionAdicional.Firma);
                cmd.Parameters.AddWithValue("IdUsuario", InformacionAdicional.Usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {

                        _result = JsonConvert.DeserializeObject<InformacionAdicionalViewModel>(dr.GetValue(0).ToString());

                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<SignatureTicketViewModel> Ticket_Procesar_Supervisor_Signature(SignatureTicketViewModel ticket)
        {
            SignatureTicketViewModel? _result = new SignatureTicketViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaSupervisor_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdTicket", ticket.Id);
                cmd.Parameters.AddWithValue("IdUsuario", ticket.Usuario.Id);
                cmd.Parameters.AddWithValue("Firma", ticket.Firma);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<SignatureTicketViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<InformacionAdicionalViewModel> UpdateCliente_Firma(InformacionAdicionalViewModel InformacionAdicional)
        {
            InformacionAdicionalViewModel? _result = new InformacionAdicionalViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.FirmaRecibido_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", InformacionAdicional.Id);
                cmd.Parameters.AddWithValue("IdTicket", InformacionAdicional.IdTicket);
                cmd.Parameters.AddWithValue("Firma", InformacionAdicional.Firma);
                cmd.Parameters.AddWithValue("IdUsuario", InformacionAdicional.Usuario.Id);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {

                        _result = JsonConvert.DeserializeObject<InformacionAdicionalViewModel>(dr.GetValue(0).ToString());

                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}
