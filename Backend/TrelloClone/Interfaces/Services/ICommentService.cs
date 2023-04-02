using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Repositories;

namespace TrelloClone.Interfaces.Services
{
    public interface ICommentService
    {
        Task<CommentDTO> CreateComment(NewCommentDTO newComment);
        Task DeleteComment(int commentId);
        Task<ICollection<CommentDTO>> GetAllCardComments(int cardId);
        Task<ICollection<CommentDTO>> GetAllComments();
        Task<ICollection<CommentDTO>> GetAllUserComments(string username);
        Task<CommentDTO> GetComment(int commentId);
        Task<bool> HasComment(int commentId);
        Task<CommentDTO> UpdateComment(CommentDTO comment);
    }
}
