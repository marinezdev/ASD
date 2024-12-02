using ASD.Areas.Administracion.Models;
using ASD.Areas.Empresa.Models;

namespace ASD.Areas.Inventario.Models
{
    public class EquipoViewModel
    {
        public int Id { get; set; }
        public SucursalViewModel? Sucursal { get; set; }
        public Cat_ModeloViewModel? Cat_Modelo { get; set; }
        public Cat_EstatusEquipoViewModel? Cat_EstatusEquipo { get; set;}
        public string? Serie { get; set; }
        public DateTime? FechaApertura { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public DateTime? FechaCaducidad { get; set; }
        public DateTime? FechaGarantia { get; set; }
        public string? Observaciones { get; set; }
        public Cat_TipoEquipoViewModel? cat_TipoEquipo { get; set; }
        public Cat_CategoriaViewModel? cat_Categoria { get; set; }
        public int? Total { get; set; } 

        public UsuarioViewModel? usuario { get; set; }
    }
}