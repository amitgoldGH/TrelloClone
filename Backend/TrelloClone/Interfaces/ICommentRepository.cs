using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface ICommentRepository
    {
        bool HasComment(int commentId);

        ICollection<Comment> GetAllComments();

        ICollection<Comment> GetAllCardComments(int cardId);

        ICollection<Comment> GetAllUserComments(string username);

        Comment GetComment(int commentId);

        Comment CreateComment(string authorName, string text, int cardId);

        Comment UpdateComment(int commentId, Comment comment);

        void DeleteComment(int commentId);
    }
}
