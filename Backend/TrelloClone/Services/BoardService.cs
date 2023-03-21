using TrelloClone.DTO;
using TrelloClone.Interfaces;
using TrelloClone.Models;

namespace TrelloClone.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMembershipService _membershipService;

        public BoardService(IBoardRepository boardRepository, IMembershipService membershipService)
        {
            _boardRepository = boardRepository;
            _membershipService = membershipService;
        }
        public Task<KanbanBoardDTO> AddMember(int boardid, string username)
        {
            throw new NotImplementedException();
        }

        public Task<KanbanBoardDTO> CreateBoard(string title, string username)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBoard(int boardid)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<KanbanBoardDTO>> GetAllBoards()
        {
            throw new NotImplementedException();
        }

        public Task<KanbanBoardDTO> GetBoard(int boardid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasBoard(int boardid)
        {
            throw new NotImplementedException();
        }

        public Task<KanbanBoardDTO> UpdateBoard(int boardid, KanbanBoard newBoard)
        {
            throw new NotImplementedException();
        }
    }
}
