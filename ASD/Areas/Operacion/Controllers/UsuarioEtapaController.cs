using ASD.Areas.Inventario.Models;
using ASD.Areas.Operacion.Models;
using ASD.Repository.Interface.Inventario;
using ASD.Repository.Interface.Operacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class UsuarioEtapaController : Controller
    {
        private readonly IUsuarioEtapaRepository _UsuarioEtapaRepository;

        public UsuarioEtapaController(IUsuarioEtapaRepository UsuarioEtapaRepository)
        {
            _UsuarioEtapaRepository = UsuarioEtapaRepository;
        }
        // Metodo para consultar las etapas de un usuario
        [HttpPost]
        public async Task<JsonResult> GetCat_Etapa_Listar_IdUsuario([FromBody] UsuarioEtapaViewModel Etapa)
        {
            List<UsuarioEtapaViewModel> R = await _UsuarioEtapaRepository.Etapa_Listar_IdUsuario(Etapa);
            return Json(R);
        }
        // Metodo para agregar una nueva etapa al usuario
        [HttpPost]
        public async Task<JsonResult> Add_UsuarioEtapa([FromBody] UsuarioEtapaViewModel Etapa)
        {
            UsuarioEtapaViewModel R = await _UsuarioEtapaRepository.Add_UsuarioEtapa(Etapa);

            return Json(R);
        }

        // Metodo para eliminar una etapa del usuario
        [HttpPost]
        public async Task<JsonResult> Delete_Etapa([FromBody] UsuarioEtapaViewModel Etapa)
        {
            UsuarioEtapaViewModel R = await _UsuarioEtapaRepository.Delete_Etapa(Etapa);

            return Json(R);
        }


        [HttpPost]
        public async Task<JsonResult> GetCat_Etapa_Listar_Faltante([FromBody] UsuarioFlujoViewModel Flujo)
        {
            List<UsuarioEtapaViewModel> R = await _UsuarioEtapaRepository.GetCat_Etapa_Listar_Faltante(Flujo);
            return Json(R);
        }
    }
}
