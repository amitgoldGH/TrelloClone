namespace TrelloClone.Interfaces.Services
{
    public interface IAssignmentService
    {
        Task<bool> AssignmentExists(string username, int cardid);

        Task AddAssignment(string username, int cardid);

        Task RemoveAssignment(string username, int cardid);
    }
}
