using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class BoardRepository : IBoardRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BoardRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // SHOULD BE DONE IN BOARD SERVICE, VIA MEMBERSHIP SERVICE.
        public Task<KanbanBoardDTO> AddMember(int boardid, string username)
        {
            throw new NotImplementedException();
        }

        public async Task<KanbanBoardDTO> CreateBoard(string title, string username)
        {

            KanbanBoard newBoard = new KanbanBoard();
            newBoard.Title = title;
            newBoard.Memberships.Add(new Membership() { BoardId = newBoard.Id, UserId = username });

            await _context.Boards.AddAsync(newBoard);
            await _context.SaveChangesAsync();

            return _mapper.Map<KanbanBoardDTO>(newBoard);
        }

        public async Task DeleteBoard(int boardid)
        {
            var board = await _context.Boards.FirstAsync(b => b.Id == boardid);
            _context.Boards.Remove(board);
        }

        public async Task<ICollection<KanbanBoardDTO>> GetAllBoards()
        {

            var boards = await _context.Boards
                .ProjectTo<KanbanBoardDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();


            return boards;

            /*return _context.Boards
                .Select(b => new KanbanBoardDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Members = b.Memberships
                        .Select(m => new BoardMembershipDTO
                        {
                            Username = m.UserId
                        }).ToList(),
                }).ToList();*/
        }

        public async Task<KanbanBoardDTO> GetBoard(int boardid)
        {
            var board = await _context.Boards
                .Where(b => b.Id == boardid)
                .ProjectTo<KanbanBoardDTO>(_mapper.ConfigurationProvider)
                .SingleAsync();

            return board;
        }

        public async Task<bool> HasBoard(int boardid)
        {
            bool boardExists = await _context.Boards.AnyAsync(b => b.Id == boardid);
            return boardExists;
        }

        public async Task<KanbanBoardDTO> UpdateBoard(int boardid, KanbanBoard newBoard)
        {
            var oldBoard = await _context.Boards.Where(b => b.Id == boardid).SingleAsync();

            oldBoard.Title = newBoard.Title;

            _context.Boards.Update(oldBoard);
            await _context.SaveChangesAsync();

            return _mapper.Map<KanbanBoardDTO>(oldBoard);
        }
    }
}
