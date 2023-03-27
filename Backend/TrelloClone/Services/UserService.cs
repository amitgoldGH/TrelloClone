using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMembershipService _membershipService;

        public UserService(IMapper mapper, IUserRepository userRepository, IMembershipService membershipService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _membershipService = membershipService;
        }


        public async Task<UserDTO> CreateUser(CredentialUserDTO newUser)
        {
            var userExists = await HasUser(newUser.Username);
            if (!userExists)
            {
                return _mapper.Map<UserDTO>(await _userRepository.CreateUser(newUser.Username, newUser.Password));
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
            var userExists = await HasUser(updatedUser.Username);
            if (userExists)
            {
                var user = await _userRepository.GetUser(updatedUser.Username);
                if (user.Password != null)
                    user.Password = updatedUser.Password;
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
    }
}
