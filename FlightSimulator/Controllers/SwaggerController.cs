using Microsoft.AspNetCore.Mvc;

namespace Listener.Controllers
{

    /// <exclude />
    public class SwaggerController : Controller
    {
        /// <exclude />
        [MenuLibrary.MenuAttributes(Name = "Swagger", Icon = "fa-book", Order = 80)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
