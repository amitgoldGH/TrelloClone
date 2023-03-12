using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface IUserRepository
    {
        bool HasUser(string username);

        UserDTO CreateUser(string username, string password);

        UserDTO GetUser(string username, Func<string, bool> existVerificationFunc);
        ICollection<User> GetUsers();

        UserDTO UpdateUser(string username, string password);

        void DeleteUser(string username);

    }
}
