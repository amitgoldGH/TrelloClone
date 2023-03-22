using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CommentDTO> CreateComment(string authorName, string text, int cardId)
        {
            Comment comment = new()
            {
                AuthorName = authorName,
                Text = text,
                CardId = cardId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<CommentDTO>> GetAllCardComments(int cardId)
        {
            var comments = await _context.Comments
                .Where(comm => comm.CardId == cardId)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return comments;
        }

        public async Task<ICollection<CommentDTO>> GetAllComments()
        {
            var comments = await _context.Comments
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return comments;
        }

        public async Task<ICollection<CommentDTO>> GetAllUserComments(string username)
        {
            var comments = await _context.Comments
                .Where(comm => comm.AuthorName == username)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return comments;
        }

        public async Task<CommentDTO> GetComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<bool> HasComment(int commentId)
        {
            var exists = await _context.Comments.AnyAsync(comm => comm.Id == commentId);

            return exists;
        }

        public async Task<CommentDTO> UpdateComment(CommentDTO updatedComment)
        {
            var oldComment = await _context.Comments.FindAsync(updatedComment.Id);

            oldComment.Text = updatedComment.Text;

            _context.Comments.Update(oldComment);

            await _context.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(oldComment);
        }
    }
}
