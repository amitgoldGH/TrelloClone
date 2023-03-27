using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface IBoardListRepository
    {
        Task<ICollection<BoardList>> GetAllBoardLists();

        Task<ICollection<BoardList>> GetSpecificBoardLists(int kanbanBoardId);

        Task<BoardList> GetSpecificList(int listId);

        Task<BoardList> CreateBoardList(string title, int boardId);

        Task<BoardList> UpdateBoardList(BoardList updateList);

        Task DeleteBoardList(int listId);

        Task<bool> HasList(int listId);
    }
}
