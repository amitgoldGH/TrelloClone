using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly DataContext _context;

        public AssignmentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAssignment(string username, int cardid)
        {
            Assignment ass = new()
            {
                CardId = cardid,
                UserId = username,
            };

            _context.Assignments.Add(ass);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AssignmentExists(string username, int cardid)
        {
            return await _context.Assignments.AnyAsync(ass => (ass.CardId == cardid) && (ass.UserId == username));
        }

        public async Task RemoveAssignment(string username, int cardid)
        {
            var ass = await _context.Assignments
                .Where(ass => (ass.CardId == cardid) && (ass.UserId == username))
                .FirstAsync();
            _context.Assignments.Remove(ass);
            await _context.SaveChangesAsync();
        }
    }
}
