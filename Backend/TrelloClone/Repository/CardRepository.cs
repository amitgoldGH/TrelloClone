using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Models;

namespace TrelloClone.Repository
{
    public class CardRepository : ICardRespository
    {
        private readonly DataContext _context;

        public CardRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Card> CreateCard(string title, string description, int boardListId)
        {
            Card card = new()
            {
                Title = title,
                Description = description,
                BoardListId = boardListId,
                Status = 0,
            };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return card;

        }

        public async Task DeleteCard(int cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<ICollection<Card>> GetAllCards()
        {
            return await _context.Cards
                .Include(c => c.Assignments).ThenInclude(ass => ass.User)
                .Include(c => c.Comments)
                .ToListAsync();
        }

        public async Task<ICollection<Card>> GetAllCardsByList(int listId)
        {
            return await _context.Cards
                .Where(card => card.BoardListId == listId)
                .Include(c => c.Assignments).ThenInclude(ass => ass.User)
                .ToListAsync();
        }

        public async Task<Card> GetCard(int cardId)
        {
            return await _context.Cards
                .Include(c => c.Assignments).ThenInclude(ass => ass.User)
                .Include(c => c.Comments)
                .FirstAsync(c => c.Id == cardId);
        }

        public async Task<bool> HasCard(int cardId)
        {
            return await _context.Cards.AnyAsync(card => card.Id == cardId);
        }

        public async Task<Card> UpdateCard(Card updatedCard)
        {

            _context.Cards.Update(updatedCard);
            await _context.SaveChangesAsync();
            return updatedCard;
        }
    }
}
