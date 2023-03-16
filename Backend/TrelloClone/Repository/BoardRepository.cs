using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class BoardRepository : IBoardRepository
    {

        private readonly DataContext _context;
        public BoardRepository(DataContext context)
        {
            _context = context;
        }

        public KanbanBoard AddMember(int boardid, string username)
        {
            throw new NotImplementedException();
        }

        public KanbanBoard CreateBoard(string title, string username)
        {
            throw new NotImplementedException();
        }

        public void DeleteBoard(int boardid)
        {
            throw new NotImplementedException();
        }

        public ICollection<KanbanBoardDTO> GetAllBoards()
        {
            return _context.Boards
                .Select(b => new KanbanBoardDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Members = b.Memberships
                        .Select(m => new BoardMembershipDTO
                        {
                            Username = m.UserId
                        }).ToList(),
                }).ToList();
        }

        public KanbanBoard GetBoard(int boardid)
        {
            throw new NotImplementedException();
        }

        public bool HasBoard(int boardid)
        {
            throw new NotImplementedException();
        }

        public KanbanBoard UpdateBoard(int boardid, KanbanBoard newBoard)
        {
            throw new NotImplementedException();
        }
    }
}
