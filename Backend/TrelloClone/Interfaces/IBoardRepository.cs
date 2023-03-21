using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface IBoardRepository
    {
        Task<bool> HasBoard(int boardid);
        Task<ICollection<KanbanBoardDTO>> GetAllBoards();

        //ICollection<KanbanBoard> GetAllUserBoards(string username); // GetUser does this already.

        Task<KanbanBoardDTO> GetBoard(int boardid);

        Task<KanbanBoardDTO> AddMember(int boardid, string username);

        Task<KanbanBoardDTO> CreateBoard(string title, string username);

        Task<KanbanBoardDTO> UpdateBoard(int boardid, KanbanBoard newBoard);
        Task DeleteBoard(int boardid);


    }
}
