using TrelloClone.DTO;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces;

namespace TrelloClone.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUser(CredentialUserDTO newUser)
        {
            var userExists = await _userRepository.HasUser(newUser.Username);
            if (!userExists)
            {
                return await _userRepository.CreateUser(newUser);
            }
            else
                throw new Exception("User already exists.");
        }

        public async Task DeleteUser(string username)
        {
            var userExists = await _userRepository.HasUser(username);
            if (userExists)
                await _userRepository.DeleteUser(username);

        }

        public async Task<UserDTO> GetUser(string username)
        {
            var userExists = await _userRepository.HasUser(username);
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
            return await _userRepository.UpdateUser(updatedUser);
        }
    }
}
