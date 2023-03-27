using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task AddAssignment(string username, int cardid)
        {
            var assExists = await AssignmentExists(username, cardid);
            if (!assExists)
                await _assignmentRepository.AddAssignment(username, cardid);
        }

        public async Task<bool> AssignmentExists(string username, int cardid)
        {
            return await _assignmentRepository.AssignmentExists(username, cardid);
        }

        public async Task RemoveAssignment(string username, int cardid)
        {
            var assExists = await AssignmentExists(username, cardid);
            if (assExists)
                await _assignmentRepository.RemoveAssignment(username, cardid);
        }
    }
}
