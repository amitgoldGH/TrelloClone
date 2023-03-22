using TrelloClone.Interfaces.Repositories;

namespace TrelloClone.Interfaces.Services
{
    public interface IMembershipService

    {
        Task AddMembership(string username, int boardId);

        Task<bool> MembershipExists(string username, int boardId);

        Task RemoveMembership(string username, int boardId);
    }
}
