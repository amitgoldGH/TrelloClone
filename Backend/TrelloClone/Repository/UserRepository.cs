using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        //DONE
        public async Task<User> CreateUser(string username, string password)
        {

            var user = new User { Username = username, Password = password, };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;

        }
        //DONE
        public async Task DeleteUser(string username)
        {
            var user = await _context.Users.FirstAsync(u => u.Username == username);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        //DONE
        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users
                .Include(u => u.Memberships)
                .ThenInclude(m => m.KanbanBoard)
                .FirstAsync(u => u.Username == username);
            return user;

            /*return _context.Users
                .Where(u => u.Username == username)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstAsync();*/

        }
        //DONE
        public async Task<ICollection<User>> GetUsers()
        {
            var users = await _context.Users
             .Include(u => u.Memberships)
             .ThenInclude(m => m.KanbanBoard)
             .ToListAsync();

            return users;

        }
        //DONE
        public async Task<bool> HasUser(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        //DONE
        public async Task<User> UpdateUser(User updatedUser)
        {

            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();
            return updatedUser;
        }


    }
}
