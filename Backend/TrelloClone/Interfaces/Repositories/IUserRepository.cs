using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> HasUser(string username);

        Task<UserDTO> CreateUser(string username, string password);

        Task<UserDTO> GetUser(string username);

        Task<ICollection<UserDTO>> GetUsers();

        Task<UserDTO> UpdateUser(string username, string newPassword);

        Task DeleteUser(string username);

    }
}
