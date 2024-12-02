using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Operacion.Models
{
    public class UsuarioFlujoViewModel
    {
        public int Id { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public FlujoViewModel? Flujo { get; set; }
    }
}
