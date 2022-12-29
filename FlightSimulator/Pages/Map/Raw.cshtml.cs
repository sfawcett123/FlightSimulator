using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Listener.Pages.Maps
{
    /// <exclude />
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
      
        /// <exclude />
        public string ApiKey { get; }

        /// <exclude />
        public IndexModel(IConfiguration configuration )
        {   
            this.configuration = configuration;
            ApiKey = this.configuration["Google:Maps:API_KEY"];
        }

        /// <exclude />
        public void OnGet()
        {
            
        }
    }
}
