using TrelloClone.DTO;
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

        Task<KanbanBoardShortDTO> CreateBoard(string title, string username);

        Task DeleteBoard(int boardid);

        Task<ICollection<KanbanBoardShortDTO>> GetAllBoards();

        Task<KanbanBoardShortDTO> GetBoard(int boardid);

        Task<bool> HasBoard(int boardid);

        Task<KanbanBoardShortDTO> UpdateBoard(KanbanBoardShortDTO newBoard);
    }
}
