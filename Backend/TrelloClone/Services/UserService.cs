using TrelloClone.DTO;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMembershipService _membershipService;

        public UserService(IUserRepository userRepository, IMembershipService membershipService)
        {
            _userRepository = userRepository;
            _membershipService = membershipService;
        }


        public async Task<UserDTO> CreateUser(CredentialUserDTO newUser)
        {
            var userExists = await HasUser(newUser.Username);
            if (!userExists)
            {
                return await _userRepository.CreateUser(newUser.Username, newUser.Password);
            }
            else
                throw new UserAlreadyExistsException();
        }

        public async Task DeleteUser(string username)
        {
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
                await _userRepository.DeleteUser(username);
            }

        }

        public async Task<UserDTO> GetUser(string username)
        {
            var userExists = await HasUser(username);
            if (userExists)
                return await _userRepository.GetUser(username);
            else
                throw new UserNotFoundException();
        }

        public async Task<ICollection<UserDTO>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserDTO> UpdateUser(CredentialUserDTO updatedUser)
        {
            var userExists = await HasUser(updatedUser.Username);
            if (userExists)
                return await _userRepository.UpdateUser(updatedUser.Username, updatedUser.Password);
            else
                throw new UserNotFoundException();
        }

        public async Task<bool> HasUser(string username)
        {
            var userExists = await _userRepository.HasUser(username);
            return userExists;
        }
    }
}
