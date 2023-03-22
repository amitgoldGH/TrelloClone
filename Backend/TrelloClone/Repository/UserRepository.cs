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
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //DONE
        public async Task<UserDTO> CreateUser(string username, string password)
        {

            var user = new User { Username = username, Password = password, };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Username = username,
            };

        }
        //DONE
        public async Task DeleteUser(string username)
        {
            var user = await _context.Users.FirstAsync(u => u.Username == username);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        //DONE
        public async Task<UserDTO> GetUser(string username)
        {
            var user = await _context.Users.FindAsync(username);
            return _mapper.Map<UserDTO>(user);

            /*return _context.Users
                .Where(u => u.Username == username)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .FirstAsync();*/

        }
        //DONE
        public async Task<ICollection<UserDTO>> GetUsers()
        {
            var users = await _context.Users
             .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
             .ToListAsync();

            return users;

        }
        //DONE
        public async Task<bool> HasUser(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        //DONE
        public async Task<UserDTO> UpdateUser(string username, string newPassword)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            user.Password = newPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);


        }


    }
}
