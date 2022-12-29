using Microsoft.AspNetCore.Mvc;

namespace Listener.Controllers
{
    /// <exclude />
    public class MapController : Controller
    {
        /// <exclude />      
        public IActionResult Raw()
        {
            return View();
        }

        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Map", Icon = "fa-map", Order = 5)]
        public IActionResult Index()
        {
            return View();
        }

    }
}
