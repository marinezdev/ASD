using ASD.Areas.Inventario.Models;

namespace ASD.Repository.Interface.Ticket
{
    public interface ICat_EquipoRutinaRepository
    {
        Task<Cat_EquipoRutinaViewModel> CreateCat_EquipoRutina(Cat_EquipoRutinaViewModel cat_EquipoRutina);
    }
}
