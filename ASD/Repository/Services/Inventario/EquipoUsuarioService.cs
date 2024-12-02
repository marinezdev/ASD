using ASD.Areas.Inventario.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using ASD.Repository.Interface.Inventario;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Persona.Models.Consulta;

namespace ASD.Repository.Services.Inventario
{
    public class EquipoUsuarioService : IEquipoUsuarioRepository
    {
        private readonly string? _cadenaSQL = string.Empty;

        public EquipoUsuarioService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<EquipoUsuarioViewModel> CreateEquipoUsuario(EquipoUsuarioViewModel equipoUsuario)
        {
            EquipoUsuarioViewModel? _result = new EquipoUsuarioViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Equipo_Usuario_Crear", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdModelo", equipoUsuario.Equipo.Cat_Modelo.Id);
                cmd.Parameters.AddWithValue("IdEstatus", equipoUsuario.Equipo.Cat_EstatusEquipo.Id);
                cmd.Parameters.AddWithValue("Serie", equipoUsuario.Equipo.Serie);
                cmd.Parameters.AddWithValue("FechaApertura", equipoUsuario.Equipo.FechaApertura);
                cmd.Parameters.AddWithValue("FechaAdquisicion", equipoUsuario.Equipo.FechaAdquisicion);
                cmd.Parameters.AddWithValue("FechaCaducidad", equipoUsuario.Equipo.FechaCaducidad);
                cmd.Parameters.AddWithValue("FechaGarantia", equipoUsuario.Equipo.FechaGarantia);
                cmd.Parameters.AddWithValue("Observaciones", equipoUsuario.Equipo.Observaciones);
                cmd.Parameters.AddWithValue("IdUsuario", equipoUsuario.Equipo.usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<EquipoUsuarioViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<List<EquipoViewModel>> GetEquipoUsuario_Seleccionar_IdUsuario(UsuarioViewModel usuario)
        {
            List<EquipoViewModel>? _result = new List<EquipoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.EquipoUsuario_Seleccionar_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EquipoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        //Función enlistar equipos por usuario

        public async Task<List<EquipoViewModel>> GetEquipo_Seleccionar_Id(EquipoUsuarioViewModel usuario)
        {
            List<EquipoViewModel>? _result = new List<EquipoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.EquipoUsuario_Seleccionar_IdUsuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUsuario", usuario.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EquipoViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }



        public async Task<List<EquipoUsuarioViewModel>> GetPersonaListar()
        {
            List<EquipoUsuarioViewModel>? _result = new List<EquipoUsuarioViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Inventario.Equipo_Usuario_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<EquipoUsuarioViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }



    }
}
