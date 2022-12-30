using FlightSimulator.Properties;
using Listener.Helpers;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;

namespace Listener.Pages
{
    /// <exclude />
    public class IndexModel : ApplicationData
    {
        /// <exclude />
        public string Document { get; set; } = string.Empty;
        /// <exclude />
        public IndexModel()
        {
        }
        /// <exclude />
        public void OnGet()
        {
            Document = PopulateDocument();
        }
        private static string PopulateDocument()
        {
            byte[] text = Resources.README;

            return Markdown.ToHtml(System.Text.Encoding.ASCII.GetString(text));
        }
    }
}