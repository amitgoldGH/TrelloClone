using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;

namespace TrelloClone.Interfaces.Services
{
    public interface IBoardListService
    {
        Task<ICollection<BoardListDTO>> GetAllBoardLists();

        Task<ICollection<BoardListDTO>> GetSpecificBoardLists(int kanbanBoardId);

        Task<BoardListDTO> GetBoardList(int listId);

        Task<BoardListDTO> CreateBoardList(NewBoardListDTO newBoardList);

        Task<BoardListDTO> UpdateBoardList(BoardListDTO updatedList);

        Task DeleteBoardList(int listId);
    }
}
