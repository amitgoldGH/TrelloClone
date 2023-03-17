using TrelloClone.DTO;

namespace TrelloClone.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser(CredentialUserDTO newUser);

        Task<UserDTO> GetUser(string username);

        Task<ICollection<UserDTO>> GetUsers();

        Task<UserDTO> UpdateUser(CredentialUserDTO updatedUser);

        Task DeleteUser(string username);

    }
}
