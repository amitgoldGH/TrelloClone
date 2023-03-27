using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class BoardListRepository : IBoardListRepository
    {
        private readonly DataContext _context;

        public BoardListRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<BoardList> CreateBoardList(string title, int boardId)
        {
            BoardList newList = new()
            {
                BoardId = boardId,
                Title = title,
            };
            _context.BoardLists.Add(newList);
            await _context.SaveChangesAsync();
            return newList;
        }

        public async Task DeleteBoardList(int listId)
        {
            var list = await _context.BoardLists.FindAsync(listId);
            if (list != null)
            {
                _context.BoardLists.Remove(list);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<BoardList>> GetAllBoardLists()
        {
            var lists = await _context.BoardLists
                .Include(bList => bList.Cards)
                .ToListAsync();
            return lists;
        }

        public async Task<ICollection<BoardList>> GetSpecificBoardLists(int kanbanBoardId)
        {
            var lists = await _context.BoardLists
                .Where(bList => bList.BoardId == kanbanBoardId)
                .Include(bList => bList.Cards)
                .ToListAsync();
            return lists;
        }

        public async Task<BoardList> GetSpecificList(int listId)
        {
            var list = await _context.BoardLists
                .Where(bList => bList.Id == listId)
                .Include(bList => bList.Cards)
                .FirstAsync();
            return list;
        }

        public async Task<bool> HasList(int listId)
        {
            return await _context.BoardLists.AnyAsync(bList => bList.Id == listId);
        }

        public async Task<BoardList> UpdateBoardList(BoardList updatedList)
        {
            _context.BoardLists.Update(updatedList);
            await _context.SaveChangesAsync();
            return updatedList;
        }
    }
}
