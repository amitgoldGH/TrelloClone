using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.Interfaces;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MembershipRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddMembership(string username, int boardId)
        {
            Membership membership = new Membership { UserId = username, BoardId = boardId };
            await _context.Memberships.AddAsync(membership);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MembershipExists(string username, int boardId)
        {
            return await _context.Memberships.AnyAsync(m => (m.UserId == username && m.BoardId == boardId));
        }

        public async Task RemoveMembership(string username, int boardId)
        {
            Membership membership = await _context.Memberships
                .FirstAsync(m => (m.UserId == username && m.BoardId == boardId));

            _context.Memberships.Remove(membership);
            await _context.SaveChangesAsync();
        }
    }
}
