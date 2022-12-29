using Microsoft.AspNetCore.Mvc;

namespace Listener.Controllers
{
    /// <exclude />
    public class BoardController : Controller
    {
        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Boards", Icon = "fa-cloud", Order = 20)]
        public IActionResult List()
        {
            return View();
        }

    }
}
