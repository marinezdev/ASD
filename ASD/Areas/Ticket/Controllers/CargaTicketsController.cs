using ASD.Areas.Administracion.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASD.Areas.Ticket.Controllers
{
    [Authorize]
    [Area("Ticket")]
    public class CargaTicketsController : Controller
    {
        private readonly ILogger<CargaTicketsController> _logger;
        public CargaTicketsController(ILogger<CargaTicketsController> logger)
        {
            _logger = logger;
        }
            
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ImportarExcel(IFormFile excel)
        {
            var workbook = new XLWorkbook(excel.OpenReadStream());
            var hoja = workbook.Worksheet(1);
            var primeraFilaUsada = hoja.FirstRowUsed().RangeAddress.FirstAddress.RowNumber;
            var ultimaFilaUsada = hoja.LastRowUsed().RangeAddress.FirstAddress.RowNumber;
            string perfil = string.Empty;
            string viewResutl = string.Empty;
            bool _formatoValido = true;




            ViewData["ExpandUserImportExcelTypeMsg"] = "Information";
            ViewData["ExpandUserImportExcelMsg"] = "Se cargaron los usuarios solicitados";
            return View(viewResutl);
        }
    }
}
