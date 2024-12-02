using System;
using ASD.Areas.Administracion.Models;
using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Administracion
{
	public interface ICat_RolRepository
	{
        Task<List<Cat_RolViewModel>> GetCat_Cat_Rol_Listar_IdUsuario(Cat_RolViewModel Cat_Rol);

    }
}

