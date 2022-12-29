using Listener.Helpers;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Listener.Pages
{
    /// <exclude />
    public class IndexModel : ApplicationData
    {
        private readonly ILogger<IndexModel> _logger;
        /// <exclude />
        public string Document { get; set; } = string.Empty;
        /// <exclude />
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        /// <exclude />
        public void OnGet()
        {
            Document = PopulateDocument();
        }
        private static string PopulateDocument()
        {
            byte[] text = Properties.Resources.README;

            return Markdown.ToHtml(System.Text.Encoding.ASCII.GetString(text));
        }
    }
}