using BoardManager;
using Listener.Workers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Listener.Pages.Boards
{
    /// <exclude />
    public class ListModel : PageModel
    {
        /// <exclude />
        public List<BoardDetails> Boards { get; private set; }
        /// <exclude />
        public BoardFactory BoardFactory { get; private set; }

        /// <exclude />
        public ListModel(BoardFactory boardFactory)
        {
            this.BoardFactory = boardFactory;
            this.Boards= new List<BoardDetails>();  
        }

        /// <exclude />
        public void OnGet()
        {
            Boards = BoardFactory.GetBoards();
        }
    }
}
