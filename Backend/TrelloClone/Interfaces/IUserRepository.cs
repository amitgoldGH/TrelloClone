﻿using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> HasUser(string username);

        Task<UserDTO> CreateUser(CredentialUserDTO newUser);

        Task<UserDTO> GetUser(string username);

        Task<ICollection<UserDTO>> GetUsers();

        Task<UserDTO> UpdateUser(CredentialUserDTO updatedUser);

        Task DeleteUser(string username);

    }
}
