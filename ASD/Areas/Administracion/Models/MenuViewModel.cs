using System;
namespace ASD.Areas.Administracion.Models
{
	public class MenuViewModel
	{
        public int Id { get; set; }
        public string? URL { get; set; }
        public Cat_RolViewModel Rol { get; set; }
    }
}

