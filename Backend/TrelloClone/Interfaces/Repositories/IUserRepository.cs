using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> HasUser(string username);

        Task<User> CreateUser(string username, string password);

        Task<User> GetUser(string username);

        Task<ICollection<User>> GetUsers();

        Task<User> UpdateUser(User updatedUser);

        Task DeleteUser(string username);

        Task UpdateRole(string username, string roleName);

    }
}
