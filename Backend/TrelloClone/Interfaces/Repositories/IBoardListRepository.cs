using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface IBoardListRepository
    {
        Task<ICollection<BoardListDTO>> GetAllBoardLists();

        Task<ICollection<BoardListDTO>> GetSpecificBoardLists(int kanbanBoardId);

        Task<BoardListDTO> GetSpecificList(int listId);

        Task<BoardListDTO> CreateBoardList(string title);

        Task<BoardListDTO> UpdateBoardList(int listId, string newTitle);

        Task DeleteBoardList(int listId);

        Task<bool> HasList(int listId);
    }
}
