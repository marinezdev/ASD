using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class EtapaController : Controller
    {
        // metodo para consultar las etapas de un flujo -- cargar en el modal 

    }
}
