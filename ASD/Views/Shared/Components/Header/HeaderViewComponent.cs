using Microsoft.AspNetCore.Mvc;

namespace ASD.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
