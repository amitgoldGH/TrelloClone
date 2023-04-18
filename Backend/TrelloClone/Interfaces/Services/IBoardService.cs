using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.DTO.Update;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Services
{
    public interface IBoardService
    {

        Task AddMember(string username, int boardid);

        Task RemoveMember(string username, int boardid);

        Task AddMembers(string[] usernames, int boardid);

        Task RemoveMembers(string[] usernames, int boardid);

        Task<BoardDTO> CreateBoard(NewKanbanDTO newKanban);

        Task DeleteBoard(int boardid);

        Task<ICollection<BoardDTO>> GetAllBoards();

        Task<BoardDTO> GetBoard(int boardid);

        Task<bool> HasBoard(int boardid);

        Task<BoardDTO> UpdateBoard(UpdateKanbanBoardDTO updatedBoard);

        Task<ICollection<BoardDisplayDTO>> GetAllDisplayBoards();
        Task<BoardDisplayDTO> GetDisplayBoard(int boardid);

        Task<bool> CheckUserActionAllowed(RequestInitiatorDTO initiator, int boardId);


    }
}
