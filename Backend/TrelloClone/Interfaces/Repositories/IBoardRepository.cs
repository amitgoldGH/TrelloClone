using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface IBoardRepository
    {
        Task<bool> HasBoard(int boardid);
        Task<ICollection<KanbanBoard>> GetAllBoards();

        //ICollection<KanbanBoard> GetAllUserBoards(string username); // GetUser does this already.

        Task<KanbanBoard> GetBoard(int boardid);

        // Task<KanbanBoardDTO> AddMember(int boardid, string username); // Done via membership service in boardservice.

        Task<KanbanBoard> CreateBoard(string title);

        Task<KanbanBoard> UpdateBoard(KanbanBoard updatedBoard);
        Task DeleteBoard(int boardid);


    }
}
