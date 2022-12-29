using Microsoft.AspNetCore.Mvc;

namespace Listener.Controllers
{
    /// <exclude />
    public class DebugController : Controller
    { 
        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Debug", Icon = "fa-bug", Order = 95)]
        public IActionResult Index()
        {
            return RedirectToAction(  "ListEndPoints" , "Debug");
        }

        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Endpoints", Icon = "fa-bug", Order = 95, Parent = "Debug")]
        public IActionResult ListEndPoints()
        {
            return View();
        }

        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Assemblies", Icon = "fa-bug", Order = 95, Parent = "Debug")]
        public IActionResult ListAssemblies()
        {
            return View();
        }
    }
}
