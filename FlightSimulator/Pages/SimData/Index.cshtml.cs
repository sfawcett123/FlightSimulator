using Listener.Workers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Listener.Pages.SimData
{
    /// <exclude />
    public class IndexModel : PageModel
    {
        /// <exclude />
        private readonly SimulatorFactory simulator;

        public Dictionary<string, string> SimData { get; private set; }

        /// <exclude />
        public IndexModel( SimulatorFactory simulator)
        {
            this.simulator = simulator;
        }      
        /// <exclude />
        public void OnGet()
        {
            SimData = simulator.AircraftData();
        }
    }
}
