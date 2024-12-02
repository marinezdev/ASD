using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace ASD.Areas.Operacion.Controllers
{
    [Authorize]
    [Area("Operacion")]
    public class UsuarioFlujoController : Controller
    {
        //SE intento poner los flujos aqui pero dan error

        // Metodo para consultar los flujo de un usuario

        // Metodo para agregar un nuevo flujo al usuario

        // Metodo para eliminar un flujo al usuario
    }
}
