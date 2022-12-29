using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Listener.Pages
{
    /// <exclude />
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        /// <exclude />
        public string? RequestId { get; set; }

        /// <exclude />
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <exclude />
        public ErrorModel()
        {
        }

        /// <exclude />
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}