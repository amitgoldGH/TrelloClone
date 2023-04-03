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

        public CommentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateComment(string authorName, string text, int cardId)
        {
            Comment comment = new()
            {
                AuthorName = authorName,
                Text = text,
                CardId = cardId
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
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

        public async Task<ICollection<Comment>> GetAllCardComments(int cardId)
        {
            var comments = await _context.Comments
                .Where(comm => comm.CardId == cardId)
                .ToListAsync();

            return comments;
        }

        public async Task<ICollection<Comment>> GetAllComments()
        {
            var comments = await _context.Comments
                .ToListAsync();

            return comments;
        }

        public async Task<ICollection<Comment>> GetAllUserComments(string username)
        {
            var comments = await _context.Comments
                .Where(comm => comm.AuthorName == username)
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> GetComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            return comment;
        }

        public async Task<bool> HasComment(int commentId)
        {
            var exists = await _context.Comments.AnyAsync(comm => comm.Id == commentId);

            return exists;
        }

        public async Task<Comment> UpdateComment(Comment updatedComment)
        {

            _context.Comments.Update(updatedComment);

            await _context.SaveChangesAsync();
            return updatedComment;
        }

        public async Task DeletedAuthor(string username)
        {
            var comments = await _context.Comments
                .Where(comm => comm.AuthorName == username)
                .ToListAsync();

            foreach (var comment in comments)
            {
                comment.AuthorName = "DELETED_USER";
                _context.Comments.Update(comment);
            }
            await _context.SaveChangesAsync();
        }
    }
}
