namespace TrelloClone.Interfaces
{
    public interface IMembershipRepository
    {
        Task<bool> MembershipExists(string username, int boardId);

        Task AddMembership(string username, int boardId);

        Task RemoveMembership(string username, int boardId);
    }
}
