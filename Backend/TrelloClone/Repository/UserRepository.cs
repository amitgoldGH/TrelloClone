using Microsoft.AspNetCore.Mvc;
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

        public UserDTO GetUser(string username)
        {

            Console.WriteLine("In GetUser: " + username);

            try
            {
                var user = _context.Users
                    .Include("Memberships")
                    .Include("Memberships.KanbanBoard")
                    .FirstOrDefault(user => user.Username == username); ;

                Console.WriteLine("Breakpointline");

                Console.WriteLine("Username: {0} is a member of {1} boards.", username, user.Memberships.Count());
                
                
                /*Console.WriteLine("Username: " + username + ", ID of boards with membership:"); */

                /*var count = user.Memberships.Count();*/

                // TODO: Map the Board IDs via Board repository in future.

                /*int[] membershipIdArray = new int[count];

                for (int i = 0; i < count; i++)
                {
                    membershipIdArray[i] = user.Memberships.ElementAt(i).BoardId;
                }*/

                /*return new UserDTO(user.Username, membershipIdArray);*/

                return new UserDTO(user.Username, user.Memberships);

            }
            catch (Exception ex)
            {

                throw new Exception("User not found!");
            }

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
