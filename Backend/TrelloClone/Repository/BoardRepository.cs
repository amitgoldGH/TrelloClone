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

        public BoardRepository(DataContext context)
        {
            _context = context;
        }

        // SHOULD BE DONE IN BOARD SERVICE, VIA MEMBERSHIP SERVICE.
        /*public Task<KanbanBoardDTO> AddMember(int boardid, string username)
        {
            throw new NotImplementedException();
        }*/

        public async Task<KanbanBoard> CreateBoard(string title)
        {

            KanbanBoard newBoard = new() { Title = title };

            //newBoard.Memberships.Add(new Membership() { BoardId = newBoard.Id, UserId = username });

            await _context.Boards.AddAsync(newBoard);
            await _context.SaveChangesAsync();

            return newBoard;
        }

        public async Task DeleteBoard(int boardid)
        {
            var board = await _context.Boards.FindAsync(boardid);
            if (board != null)
            {
                _context.Boards.Remove(board);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<KanbanBoard>> GetAllBoards()
        {

            var boards = await _context.Boards
                .Include(b => b.Memberships).ThenInclude(m => m.User)
                .Include(b => b.BoardLists).ThenInclude(bList => bList.Cards)
                .ToListAsync();


            return boards;
        }

        public async Task<KanbanBoard> GetBoard(int boardid)
        {
            var board = await _context.Boards
                .Include(b => b.Memberships).ThenInclude(m => m.User)
                .Include(b => b.BoardLists).ThenInclude(bList => bList.Cards)
                .FirstAsync(b => b.Id == boardid);

            return board;


        }

        public async Task<bool> HasBoard(int boardid)
        {
            bool boardExists = await _context.Boards.AnyAsync(b => b.Id == boardid);
            return boardExists;
        }

        public async Task<KanbanBoard> UpdateBoard(KanbanBoard updatedBoard)
        {

            _context.Boards.Update(updatedBoard);
            await _context.SaveChangesAsync();

            return updatedBoard;
        }
    }
}
