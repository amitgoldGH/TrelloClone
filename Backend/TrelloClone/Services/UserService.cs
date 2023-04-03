using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Exceptions;
using TrelloClone.Helper;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMembershipService _membershipService;
        private readonly ICommentRepository _commentRepository;
        private readonly IAssignmentService _assignmentService;

        public UserService(IMapper mapper, IUserRepository userRepository, IMembershipService membershipService, IAssignmentService assignmentService, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _membershipService = membershipService;
            _commentRepository = commentRepository;
            _assignmentService = assignmentService;
        }


        public async Task<UserDTO> CreateUser(CredentialUserDTO newUser)
        {

            if (newUser == null || Helper.Helper.illegalStringCheck(newUser.Username))
            {
                throw new UserBadRequestException();
            }

            newUser.Username = newUser.Username.ToLower();
            var userExists = await HasUser(newUser.Username);
            if (!userExists)
            {
                return _mapper
                    .Map<UserDTO>(await _userRepository
                    .CreateUser(
                        newUser.Username,
                        Helper.Helper.HashString(newUser.Password)
                    ));
            }
            else
                throw new UserAlreadyExistsException();


        }

        public async Task DeleteUser(string username)
        {


            username = username.ToLower();

            var userExists = await HasUser(username);
            if (userExists)
            {
                var user = await _userRepository.GetUser(username);
                if (user.Memberships.Count > 0)
                {
                    foreach (var mem in user.Memberships)
                    {
                        await _membershipService.RemoveMembership(username, mem.BoardId);
                    }
                }
                if (user.Assignments.Count > 0)
                {
                    foreach (var ass in user.Assignments)
                    {
                        await _assignmentService.RemoveAssignment(username, ass.CardId);
                    }
                }
                await _commentRepository.DeletedAuthor(username); // Changes all comments by the author to "DELETED_USER"
                await _userRepository.DeleteUser(username);
            }

        }

        public async Task<UserDTO> GetUser(string username)
        {
            if (Helper.Helper.illegalStringCheck(username))
            {
                throw new UserBadRequestException();
            }

            username = username.ToLower();

            var userExists = await HasUser(username);
            if (userExists)
                return _mapper.Map<UserDTO>(await _userRepository.GetUser(username));
            else
                throw new UserNotFoundException();
        }

        public async Task<ICollection<UserDTO>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> UpdateUser(CredentialUserDTO updatedUser)
        {
            if (updatedUser == null || Helper.Helper.illegalStringCheck(updatedUser.Username))
            {
                throw new UserBadRequestException();
            }
            var userExists = await HasUser(updatedUser.Username);
            if (userExists)
            {
                var user = await _userRepository.GetUser(updatedUser.Username);
                if (user.Password != null)
                {
                    user.Password = Helper.Helper.HashString(updatedUser.Password);
                }
                return _mapper.Map<UserDTO>(await _userRepository.UpdateUser(user));

            }
            else
                throw new UserNotFoundException();
        }

        public async Task<bool> HasUser(string username)
        {
            var userExists = await _userRepository.HasUser(username);
            return userExists;
        }

        public async Task<UserDTO> Login(CredentialUserDTO userLogin)
        {

            if (userLogin == null || Helper.Helper.illegalStringCheck(userLogin.Username))
            {
                throw new UserBadRequestException();
            }

            userLogin.Username = userLogin.Username.ToLower();

            var userExists = await HasUser(userLogin.Username);
            if (userExists)
            {
                var foundUser = await _userRepository.GetUser(userLogin.Username);
                if (foundUser.Password == Helper.Helper.HashString(userLogin.Password))
                {
                    return _mapper.Map<UserDTO>(foundUser);
                }
                else
                {
                    throw new UserIncorrectLogin();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public async Task Updaterole(string username, string roleName)
        {
            if (Helper.Helper.illegalStringCheck(username))
                throw new UserBadRequestException("Illegal username");

            username = username.ToLower();

            var userExists = await HasUser(username);
            if (userExists)
            {
                if (roleName.Equals("Admin") || roleName.Equals("User"))
                {
                    await _userRepository.UpdateRole(username, roleName);
                }
                else
                {
                    throw new UserBadRequestException("Invalid role name");
                }
            }
            else
            {
                throw new UserNotFoundException("User not found");
            }

        }
    }
}
