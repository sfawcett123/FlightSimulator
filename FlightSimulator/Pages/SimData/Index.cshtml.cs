using Listener.Workers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Listener.Pages.SimData
{
    /// <exclude />
    public class IndexModel : PageModel
    {
        /// <exclude />
        public Dictionary<string, string> SimData { get; private set; }

        /// <exclude />
        public IndexModel( SimulatorFactory simulator)
        {
            SimData = simulator.AircraftData();
        }      
        /// <exclude />
        public void OnGet()
        {
            
        }
    }
}
