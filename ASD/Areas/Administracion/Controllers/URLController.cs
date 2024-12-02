using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASD.Areas.Administracion.Models;
using static ASD.Repository.Interface.Administracion.IUrlRepository;

namespace ASD.Areas.Administracion.Controllers
{
    [Area("Administracion")]
    [Authorize]

    public class URLController : Controller
    {
        
        [HttpPost]
        public async Task<JsonResult> URL_Cifrar([FromBody] UrlViewModel MyUrl)
        {
            UrlViewModel newUrl = new UrlViewModel();

            newUrl.Url =  Cifrado.Encriptar(MyUrl.UrlVariable);

            return Json(newUrl);
        }
    }
}