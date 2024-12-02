namespace ASD.Areas.Empresa.Models
{
    public class SucursalViewModel
    {
        public int Id { get; set; }
        public EmpresaViewModel? Empresa { get; set; }
        public Cat_ColoniaViewModel? Cat_Colonia { get; set; }
        public Cat_TipoSucursalViewModel? Cat_TipoSucursal { get; set; } 
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set;}

    }
}
