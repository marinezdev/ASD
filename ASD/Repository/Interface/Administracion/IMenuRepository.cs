using System;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;

namespace ASD.Repository.Interface.Administracion
{
	public interface IMenuRepository
	{
        Task<List<MenuViewModel>> GetMenu_IdRol(MenuViewModel MenuRol);
        Task<MenuViewModel> Delete_Menu(MenuViewModel MenuRol);
        Task<List<MenuViewModel>> GetCat_Menu_Listar_Faltante(MenuViewModel MenuRol);
        Task<MenuViewModel> Insert_MenuPermiso(MenuViewModel MenuRol);
    }
}

