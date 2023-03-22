using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface IBoardRepository
    {
        Task<bool> HasBoard(int boardid);
        Task<ICollection<KanbanBoardShortDTO>> GetAllBoards();

        //ICollection<KanbanBoard> GetAllUserBoards(string username); // GetUser does this already.

        Task<KanbanBoardShortDTO> GetBoard(int boardid);

        // Task<KanbanBoardDTO> AddMember(int boardid, string username); // Done via membership service in boardservice.

        Task<KanbanBoardShortDTO> CreateBoard(string title, string username);

        Task<KanbanBoardShortDTO> UpdateBoard(int boardid, string newTitle);
        Task DeleteBoard(int boardid);


    }
}
