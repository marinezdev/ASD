using ASD.Areas.Ticket.Models;
using ASD.Repository.Interface.Operacion;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Areas.Operacion.Models;

namespace ASD.Repository.Services.Operacion
{
    public class Cat_EstatusTicketEtapaService: ICat_EstatusTicketEtapaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_EstatusTicketEtapaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<Cat_EstatusTicketEtapaViewModel>> GetCat_TipoEstatusEtapa()
        {
            List<Cat_EstatusTicketEtapaViewModel>? _result = new List<Cat_EstatusTicketEtapaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Cat_EstatusTicketEtapa_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_EstatusTicketEtapaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstatusTicketEtapaViewModel> InsertTipoEstatusEtapa(Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa)
        {
            Cat_EstatusTicketEtapaViewModel _result = new Cat_EstatusTicketEtapaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Cat_EstatusTicketEtapa_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", Cat_EstatusTicketEtapa.Nombre);
                cmd.Parameters.AddWithValue("Color", Cat_EstatusTicketEtapa.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusTicketEtapaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstatusTicketEtapaViewModel> UpdateTipoEstatusEtapa(Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa)
        {
            Cat_EstatusTicketEtapaViewModel _result = new Cat_EstatusTicketEtapaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Cat_EstatusTicketEtapa_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cat_EstatusTicketEtapa.Id);
                cmd.Parameters.AddWithValue("Nombre", Cat_EstatusTicketEtapa.Nombre);
                cmd.Parameters.AddWithValue("Color", Cat_EstatusTicketEtapa.Color);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusTicketEtapaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_EstatusTicketEtapaViewModel> DeleteTipoEstatusEtapa(Cat_EstatusTicketEtapaViewModel Cat_EstatusTicketEtapa)
        {
            Cat_EstatusTicketEtapaViewModel _result = new Cat_EstatusTicketEtapaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Operacion.Cat_EstatusTicketEtapa_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Cat_EstatusTicketEtapa.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_EstatusTicketEtapaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
