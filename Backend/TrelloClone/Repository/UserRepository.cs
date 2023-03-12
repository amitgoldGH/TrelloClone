﻿using Microsoft.EntityFrameworkCore;
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
                     .Where(x => x.Username == username)
                     .Include(user => user.Memberships).ThenInclude(mem => mem.KanbanBoard).Single();

                var memberships = user.Memberships
                    .Select(m => new MembershipDTO
                    { BoardId = m.BoardId, Title = m.KanbanBoard.Title })
                    .ToList();

                Console.WriteLine("Breakpointline");


                return new UserDTO(user.Username, memberships);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
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
