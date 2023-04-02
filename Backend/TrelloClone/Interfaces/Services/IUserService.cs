using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Repositories;

namespace TrelloClone.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> HasUser(string username);

        Task<UserDTO> CreateUser(CredentialUserDTO newUser);

        Task<UserDTO> GetUser(string username);

        Task<ICollection<UserDTO>> GetUsers();

        Task<UserDTO> UpdateUser(CredentialUserDTO updatedUser);

        Task<UserDTO> Login(CredentialUserDTO user);

        Task DeleteUser(string username);

        Task Updaterole(string username, string roleName);
    }
}
