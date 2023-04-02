using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> HasComment(int commentId);

        Task<ICollection<Comment>> GetAllComments();

        Task<ICollection<Comment>> GetAllCardComments(int cardId);

        Task<ICollection<Comment>> GetAllUserComments(string username);

        Task<Comment> GetComment(int commentId);

        Task<Comment> CreateComment(string authorName, string text, int cardId);

        Task<Comment> UpdateComment(Comment updatedComment);

        Task DeleteComment(int commentId);
    }
}
