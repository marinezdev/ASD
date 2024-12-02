using ASD.Areas.Inventario.Models;
using ASD.Areas.Persona.Models;
using System.Reflection.Metadata;

namespace ASD.Repository.Interface.Persona
{
    public interface ICat_TipoEmailRepository
    {
        Task<List<Cat_TipoEmailViewModel>> GetCat_TipoEmail_Seleccionar();
        Task<Cat_TipoEmailViewModel> CreateCat_TipoEmail(Cat_TipoEmailViewModel cat_TipoEmail);



        Task<Cat_TipoEmailViewModel> Delete_TipoEmail(Cat_TipoEmailViewModel cat_TipoEmail);
        Task<Cat_TipoEmailViewModel> Update_TipoEmail(Cat_TipoEmailViewModel cat_TipoEmail);
    }
}


