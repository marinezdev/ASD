using ASD.Areas.Empresa.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace ASD.Areas.Administracion.Models
{
	public class UsuarioViewModel
	{
		public int Id { get; set; }
		public Cat_RolViewModel? Cat_Rol { get; set; }
		public string? Password { get; set; }
		public string? Usuario { get; set; }

		public CuadrillaViewModel? Cuadrilla { get; set; }
    }
}
