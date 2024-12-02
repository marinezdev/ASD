using Microsoft.AspNetCore.Mvc;

namespace ASD.Views.Shared.Components.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
