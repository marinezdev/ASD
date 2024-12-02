using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_EquipoImagenRepository
    {
        Task<Cat_EquipoImagenViewModel> CreateCat_EquipoImagen(Cat_EquipoImagenViewModel cat_EquipoImagen);
    }
}
