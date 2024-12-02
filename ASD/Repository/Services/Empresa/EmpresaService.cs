using ASD.Areas.Empresa.Models;
using ASD.Repository.Interface.Empresa;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace ASD.Repository.Services.Empresa
{
    public class EmpresaService: IEmpresaRepository
    {
        private readonly string? _cadenaSQL = string.Empty;
        public EmpresaService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }


        public async Task<List<EmpresaViewModel>> GetEmpresa_Seleccionar()
        {
            List<EmpresaViewModel>? _result = new List<EmpresaViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Empresa.Empresa_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EmpresaViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


    }
}
