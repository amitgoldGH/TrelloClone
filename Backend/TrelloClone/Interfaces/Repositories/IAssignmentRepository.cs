namespace TrelloClone.Interfaces.Repositories
{
    public interface IAssignmentRepository
    {
        Task<bool> AssignmentExists(string username, int cardid);

        Task AddAssignment(string username, int cardid);

        Task RemoveAssignment(string username, int cardid);
    }
}
