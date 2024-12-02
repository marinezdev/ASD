using ASD.Areas.Administracion.Models;

namespace ASD.Areas.Operacion.Models
{
    public class UsuarioEtapaViewModel
    {
        public int Id { get; set; }
        public UsuarioViewModel? Usuario { get; set; }
        public EtapaViewModel? Etapa { get; set; }

    }
}
