using Microsoft.AspNetCore.Mvc;

namespace Listener.Controllers
{
    /// <exclude />
    public class SimDataController : Controller
    {
        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Simulator", Icon = "fa-plane", Order = 20)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
