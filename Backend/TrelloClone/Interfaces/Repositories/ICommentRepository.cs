using TrelloClone.DTO;

namespace TrelloClone.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> HasComment(int commentId);

        Task<ICollection<CommentDTO>> GetAllComments();

        Task<ICollection<CommentDTO>> GetAllCardComments(int cardId);

        Task<ICollection<CommentDTO>> GetAllUserComments(string username);

        Task<CommentDTO> GetComment(int commentId);

        Task<CommentDTO> CreateComment(string authorName, string text, int cardId);

        Task<CommentDTO> UpdateComment(CommentDTO updatedComment);

        Task DeleteComment(int commentId);
    }
}
