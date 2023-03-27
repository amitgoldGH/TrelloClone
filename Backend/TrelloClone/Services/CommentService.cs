using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserService _userService;

        //TODO ADD CARD SERVICE TO DEP INJECTION
        public CommentService(IMapper mapper, ICommentRepository commentRepository, IUserService userService)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public async Task<CommentDTO> CreateComment(NewCommentDTO newComment)
        {
            if (newComment == null)
                throw new NotImplementedException(); // COMMENT BAD REQUEST EXCEPTION
            var userExists = await _userService.HasUser(newComment.AuthorName);

            if (userExists)
            {
                // TODO : ADD CARD VERIFICATION + TEXT VERIFICATION
                return _mapper.Map<CommentDTO>(await _commentRepository.CreateComment(newComment.AuthorName, newComment.Text, newComment.CardId));
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
            return _mapper.Map<List<CommentDTO>>(comments);
        }

        public async Task<ICollection<CommentDTO>> GetAllComments()
        {
            var comments = await _commentRepository.GetAllComments();
            return _mapper.Map<List<CommentDTO>>(comments);
        }

        public async Task<ICollection<CommentDTO>> GetAllUserComments(string username)
        {
            var userExists = await _userService.HasUser(username);
            if (userExists)
            {
                return _mapper.Map<List<CommentDTO>>(await _commentRepository.GetAllUserComments(username));
            }
            else { throw new UserNotFoundException(); }
        }

        public async Task<CommentDTO> GetComment(int commentId)
        {
            var commentExists = await HasComment(commentId);

            if (commentExists)
            {
                return _mapper.Map<CommentDTO>(await _commentRepository.GetComment(commentId));
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
                    var comm = await _commentRepository.GetComment(comment.Id);
                    comm.Text = comment.Text;
                    comm.AuthorName = comment.AuthorName;

                    return _mapper.Map<CommentDTO>(await _commentRepository.UpdateComment(comm));
                }
                else
                {
                    throw new NotImplementedException(); // TODO COMMENT NOT FOUND
                }
            }

        }
    }
}
