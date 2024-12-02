using ASD.Areas.Ticket.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Ticket;
using ASD.Areas.Ticket.Models.Consulta;

namespace ASD.Repository.Services.Ticket
{
    public class Cat_AsignacionEmpresaService : ICat_AsignacionEmpresaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public Cat_AsignacionEmpresaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }


        public async Task<List<Cat_AsignacionEmpresaViewModel>> GetCat_AsignacionEmpresa_Seleccionar()
        {
            List<Cat_AsignacionEmpresaViewModel>? _result = new List<Cat_AsignacionEmpresaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_AsignacionEmpresa_Seleccionar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<Cat_AsignacionEmpresaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_AsignacionEmpresaViewModel> InsertAsignacionEmpresa(Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa)
        {
            Cat_AsignacionEmpresaViewModel? _result = new Cat_AsignacionEmpresaViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_AsignacionEmpresa_Insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Nombre", cat_AsignacionEmpresa.Nombre);
           

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_AsignacionEmpresaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<Cat_AsignacionEmpresaViewModel> UpdateAsignacionEmpresa(Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa)
        {
            Cat_AsignacionEmpresaViewModel _result = new Cat_AsignacionEmpresaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_AsignacionEmpresa_Actualizar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_AsignacionEmpresa.Id);
                cmd.Parameters.AddWithValue("Nombre", cat_AsignacionEmpresa.Nombre);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_AsignacionEmpresaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<Cat_AsignacionEmpresaViewModel> DeleteAsignacionEmpresa(Cat_AsignacionEmpresaViewModel cat_AsignacionEmpresa)
        {
            Cat_AsignacionEmpresaViewModel _result = new Cat_AsignacionEmpresaViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Ticket.Cat_AsignacionEmpresa_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", cat_AsignacionEmpresa.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<Cat_AsignacionEmpresaViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
    }
}
