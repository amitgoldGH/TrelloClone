using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //DONE
        public UserDTO CreateUser(string username, string password, Func<string, bool> existVerificationFunc)
        {
            if (existVerificationFunc(username))
            {
                return null;
            }
            else
            {
                var user = new User { Username = username, Password = password, };
                _context.Users.Add(user);
                _context.SaveChanges();

                return new UserDTO
                {
                    Username = username,
                };
            }
        }
        //DONE
        public void DeleteUser(string username, Func<string, bool> existVerificationFunc)
        {
            if (existVerificationFunc(username))
            {
                var user = _context.Users.First(u => u.Username == username);
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        //DONE
        public UserDTO GetUser(string username, Func<string, bool> existVerificationFunc)
        {

            if (existVerificationFunc(username))
            {
                try
                {


                    /*var memberships = _mapper
                        .Map<List<Membership>, List<UserMembershipDTO>>
                        (_context.Memberships
                            .Where(u => u.UserId == username)
                            .Include(u => u.KanbanBoard).ToList()
                        );


                    var membershi = _mapper.ProjectTo<UserMembershipDTO>
                        (_context.Memberships.Where(u => u.UserId == username).Include(u => u.KanbanBoard))
                        .ToList();*/



                    /*var memberships = _context.Memberships
                        .Where(m => m.UserId == username)
                        .Select(b => new UserMembershipDTO
                        {
                            BoardId = b.BoardId,
                            BoardTitle = b.KanbanBoard.Title
                        }).ToList();*/


                    var memberships = _context.Memberships
                        .Where(u => u.UserId == username)
                        .ProjectTo<UserMembershipDTO>(_mapper.ConfigurationProvider)
                        .ToList();

                    Console.WriteLine("TEST");


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
        //DONE
        public ICollection<UserDTO> GetUsers()
        {
            /*var users = _context.Users
                        .Select(u => new UserDTO
                        {
                            Username = u.Username,
                            Memberships = u.Memberships.Select(b => new UserMembershipDTO
                            {
                                BoardId = b.BoardId,
                                BoardTitle = b.KanbanBoard.Title
                            }).ToList()
                        }).ToList();*/


            var users = _context.Users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToList();

            Console.WriteLine("Test");


            return users.OrderBy(u => u.Username).ToList();
        }
        //DONE
        public bool HasUser(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        //DONE
        public UserDTO UpdateUser(CredentialUserDTO updatedUser, Func<string, bool> existVerificationFunc)
        {
            if (!(updatedUser == null))
            {
                if (existVerificationFunc(updatedUser.Username))
                {
                    var user = _context.Users.FirstOrDefault(u => u.Username == updatedUser.Username);
                    user.Password = updatedUser.Password;
                    _context.Users.Update(user);
                    _context.SaveChanges();


                    return new UserDTO { Username = user.Username, Memberships = _mapper.Map<List<UserMembershipDTO>>(user.Memberships) };
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
