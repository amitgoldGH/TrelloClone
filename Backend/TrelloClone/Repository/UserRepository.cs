using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces;
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

        public UserDTO CreateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetUser(string username, Func<string, bool> existVerificationFunc)
        {

            if (existVerificationFunc(username))
            {
                try
                {
                    var memberships = _context.Memberships
                        .Where(m => m.UserId == username)
                        .Select(b => new MembershipDTO
                        {
                            BoardId = b.BoardId,
                            Title = b.KanbanBoard.Title
                        }).ToList();


                    return new UserDTO(username, memberships);

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            else
                return null;


        }

        public ICollection<User> GetUsers()
        {

            Console.WriteLine("In GetUsers");

            return _context.Users.OrderBy(u => u.Username).ToList();
        }

        public bool HasUser(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public UserDTO UpdateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
