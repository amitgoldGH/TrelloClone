using TrelloClone.DTO;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserService _userService;

        //TODO ADD CARD SERVICE TO DEP INJECTION
        public CommentService(ICommentRepository commentRepository, IUserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public async Task<CommentDTO> CreateComment(string authorName, string text, int cardId)
        {
            var userExists = await _userService.HasUser(authorName);

            if (userExists)
            {
                // TODO : ADD CARD VERIFICATION + TEXT VERIFICATION
                return await _commentRepository.CreateComment(authorName, text, cardId);
            }
            else
            {
                throw new UserNotFoundException();
            }

        }

        public async Task DeleteComment(int commentId)
        {
            var commExists = await _commentRepository.HasComment(commentId);
            if (commExists)
                await _commentRepository.DeleteComment(commentId);
        }

        public async Task<ICollection<CommentDTO>> GetAllCardComments(int cardId)
        {
            var comments = await _commentRepository.GetAllCardComments(cardId);
            return comments;
        }

        public async Task<ICollection<CommentDTO>> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();
            return comments;
        }

        public async Task<ICollection<CommentDTO>> GetAllUserComments(string username)
        {
            var userExists = await _userService.HasUser(username);
            if (userExists)
            {
                return await _commentRepository.GetAllUserComments(username);
            }
            else { throw new UserNotFoundException(); }
        }

        public async Task<CommentDTO> GetComment(int commentId)
        {
            var commentExists = await HasComment(commentId);

            if (commentExists)
            {
                var comment = await _commentRepository.GetComment(commentId);
                return comment;
            }
            else
                throw new NotImplementedException(); // TODO Create NOT COMMENT NOT FOUND EXCEPTION
        }

        public async Task<bool> HasComment(int commentId)
        {
            return await _commentRepository.HasComment(commentId);
        }

        public async Task<CommentDTO> UpdateComment(CommentDTO comment)
        {
            if (comment == null)
            {
                throw new NotImplementedException(); // TODO COMMENT BAD REQUEST
            }
            else
            {
                var commExists = await HasComment(comment.Id);
                if (commExists)
                {
                    return await _commentRepository.UpdateComment(comment);
                }
                else
                {
                    throw new NotImplementedException(); // TODO COMMENT NOT FOUND
                }
            }

        }
    }
}
