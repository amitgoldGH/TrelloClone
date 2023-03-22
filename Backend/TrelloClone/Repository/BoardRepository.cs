using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces.Repositories;
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
        /*public Task<KanbanBoardDTO> AddMember(int boardid, string username)
        {
            throw new NotImplementedException();
        }*/

        public async Task<KanbanBoardShortDTO> CreateBoard(string title, string username)
        {

            KanbanBoard newBoard = new KanbanBoard();
            newBoard.Title = title;
            //newBoard.Memberships.Add(new Membership() { BoardId = newBoard.Id, UserId = username });

            await _context.Boards.AddAsync(newBoard);
            await _context.SaveChangesAsync();

            return _mapper.Map<KanbanBoardShortDTO>(newBoard);
        }

        public async Task DeleteBoard(int boardid)
        {
            var board = await _context.Boards.FirstAsync(b => b.Id == boardid);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<KanbanBoardShortDTO>> GetAllBoards()
        {

            var boards = await _context.Boards
                .ProjectTo<KanbanBoardShortDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();


            return boards;
        }

        public async Task<KanbanBoardShortDTO> GetBoard(int boardid)
        {
            var board = await _context.Boards
                .FindAsync(boardid);

            return _mapper.Map<KanbanBoardShortDTO>(board);


        }

        public async Task<bool> HasBoard(int boardid)
        {
            bool boardExists = await _context.Boards.AnyAsync(b => b.Id == boardid);
            return boardExists;
        }

        public async Task<KanbanBoardShortDTO> UpdateBoard(int newBoardId, string newTitle)
        {
            var oldBoard = await _context.Boards.Where(b => b.Id == newBoardId).SingleAsync();

            oldBoard.Title = newTitle;

            _context.Boards.Update(oldBoard);
            await _context.SaveChangesAsync();

            return _mapper.Map<KanbanBoardShortDTO>(oldBoard);
        }
    }
}
