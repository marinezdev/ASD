using System;
using ASD.Areas.Administracion.Models;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.Administracion;
using Newtonsoft.Json;

namespace ASD.Repository.Services.Administracion
{
	public class PasswordResetRequestsService: IPasswordResetRequestsRepository
    {
        private readonly string _cadenaSQL = string.Empty;

        public PasswordResetRequestsService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        //PC
        public async Task<PasswordResetRequestsViewModel> PasswordResetRequests_Insert(PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel _result = new PasswordResetRequestsViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.PasswordResetRequests_Insert", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Correo", _model.Email.Email);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<PasswordResetRequestsViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<PasswordResetRequestsViewModel> PasswordResetRequests_Consult(PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel _result = new PasswordResetRequestsViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.PasswordResetRequests_Consult", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Token", _model.Token);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<PasswordResetRequestsViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<PasswordResetRequestsViewModel> Usuario_ActualizarPass(PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel _result = new PasswordResetRequestsViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Usuario_ActualizarPass", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUser", _model.Usuario.Id);
                cmd.Parameters.AddWithValue("Password", _model.Usuario.Password);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<PasswordResetRequestsViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }


        //MOVIL
        public async Task<PasswordResetRequestsViewModel> PasswordResetRequests_InsertM(PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel _result = new PasswordResetRequestsViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.PasswordResetRequests_InsertM", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Correo", _model.Email.Email);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<PasswordResetRequestsViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<PasswordResetRequestsViewModel> PasswordResetRequests_ConsultM(PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel _result = new PasswordResetRequestsViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.PasswordResetRequests_ConsultM", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Token", _model.Token);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<PasswordResetRequestsViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }
        public async Task<PasswordResetRequestsViewModel> Usuario_ActualizarPassM(PasswordResetRequestsViewModel _model)
        {
            PasswordResetRequestsViewModel _result = new PasswordResetRequestsViewModel();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Usuario_ActualizarPassM", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdUser", _model.Usuario.Id);
                cmd.Parameters.AddWithValue("Password", _model.Usuario.Password);


                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<PasswordResetRequestsViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

    }
}

