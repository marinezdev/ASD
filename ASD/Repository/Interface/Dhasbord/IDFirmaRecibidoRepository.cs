using ASD.Areas.Administracion.Models;
using ASD.Areas.Ticket.Models;

namespace ASD.Repository.Interface.Dhasbord
{
    public interface IDFirmaRecibidoRepository
    {
        Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta1(UsuarioViewModel usuario);
        Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta2(UsuarioViewModel usuario);
        Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta3(UsuarioViewModel usuario);
        Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta1();
        Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta2();
        Task<List<FirmaRecibidoViewModel>> GetDhasbord_Recibido_Encuesta_Pregunta3();
    }
}
