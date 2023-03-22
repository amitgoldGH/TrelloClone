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
        private readonly IMapper _mapper;

        public CardRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CardDTO> CreateCard(string title, string description, int boardListId)
        {
            Card card = new Card()
            {
                Title = title,
                Description = description,
                BoardListId = boardListId,
                Status = 0,
            };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();

            return _mapper.Map<CardDTO>(card);

        }

        public async Task DeleteCard(int cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

        }

        public async Task<ICollection<CardDTO>> GetAllCards()
        {
            return await _context.Cards
                .ProjectTo<CardDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ICollection<CardDTO>> GetAllCardsByList(int listId)
        {
            return await _context.Cards
                .Where(card => card.BoardListId == listId)
                .ProjectTo<CardDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CardDTO> GetCard(int cardId)
        {
            return _mapper.Map<CardDTO>(await _context.Cards.FindAsync(cardId));
        }

        public async Task<CardDTO> UpdateCard(CardDTO updatedCard)
        {
            var oldCard = await _context.Cards.FindAsync(updatedCard.Id);

            oldCard.Title = updatedCard.Title;
            oldCard.Description = updatedCard.Description;
            oldCard.Status = updatedCard.Status;

            _context.Cards.Update(oldCard);
            await _context.SaveChangesAsync();

            return _mapper.Map<CardDTO>(oldCard);
        }
    }
}
