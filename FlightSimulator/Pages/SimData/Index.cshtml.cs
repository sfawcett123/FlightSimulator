using BoardManager;
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
        private readonly BoardFactory board;

        /// <summary>Gets the sim data.</summary>
        /// <value>The sim data.</value>
        public Dictionary<string, string>? SimData { get; private set; }
        /// <summary>Gets the input data.</summary>
        /// <value>The input data.</value>
        public Dictionary<string, string>? InputData { get; private set; }
        /// <summary>Gets the list of boards.</summary>
        /// <value>The list of boards.</value>
        public List<BoardDetails>? ListOfBoards { get; private set; }

        /// <exclude />
        public IndexModel( SimulatorFactory simulator , BoardFactory boardFactory)
        {
            this.simulator = simulator;
            this.board = boardFactory;
        }      
        /// <exclude />
        public void OnGet()
        {
            SimData = simulator.AircraftData();
            InputData = board.GetAllInputData();
            ListOfBoards = board.GetBoards();
        }
    }
}
