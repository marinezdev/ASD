using System;
using ASD.Areas.Inventario.Models;
using System.Data;
using System.Data.SqlClient;
using ASD.Repository.Interface.Administracion;
using Newtonsoft.Json;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Repository.Services.Administracion
{
	public class MenuService : IMenuRepository
	{
        private readonly string _cadenaSQL = string.Empty;

        public MenuService(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("UsersAdminConectionSQL");
        }

        public async Task<List<MenuViewModel>> GetMenu_IdRol(MenuViewModel MenuRol)
        {
            List<MenuViewModel>? _result = new List<MenuViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Menu_Listar_IdRol", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", MenuRol.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<List<MenuViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }
        }

        public async Task<MenuViewModel> Delete_Menu(MenuViewModel MenuRol)
        {
            MenuViewModel _result = new MenuViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.MenuPermiso_Borrar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", MenuRol.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<MenuViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

        public async Task<List<MenuViewModel>> GetCat_Menu_Listar_Faltante(MenuViewModel MenuRol)
        {
            List<MenuViewModel> _Flujo = new List<MenuViewModel>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.Menu_Listar_Faltante", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", MenuRol.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _Flujo = JsonConvert.DeserializeObject<List<MenuViewModel>>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _Flujo;
            }
        }
        public async Task<MenuViewModel> Insert_MenuPermiso(MenuViewModel MenuRol)
        {
            MenuViewModel _result = new MenuViewModel();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("Administracion.MenuPermiso_insertar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdRol", MenuRol.Rol.Id);
                cmd.Parameters.AddWithValue("IdUrl", MenuRol.Id);

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                    {
                        _result = JsonConvert.DeserializeObject<MenuViewModel>(dr.GetValue(0).ToString());
                    }
                }
                conexion.Close();
                return _result;
            }

        }

    }
}

