using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardRespository _cardRespository;
        private readonly IAssignmentService _assignmentService;

        public CardService(IMapper mapper, ICardRespository cardRespository, IAssignmentService assignmentService)
        {
            _mapper = mapper;
            _cardRespository = cardRespository;
            _assignmentService = assignmentService;
        }

        public async Task<CardDTO> CreateCard(NewCardDTO newCard)
        {
            if (newCard == null)
                throw new NotImplementedException(); // TODO THROW BAD CARD REQUEST
            var card = await _cardRespository.CreateCard(newCard.Title, newCard.Description, newCard.BoardListId);
            return _mapper.Map<CardDTO>(card);
        }

        public async Task DeleteCard(int cardId)
        {
            var cardExists = await _cardRespository.HasCard(cardId);
            if (cardExists)
            {
                await _cardRespository.DeleteCard(cardId);
            }
        }

        public async Task<ICollection<CardDTO>> GetAllCards()
        {
            var cards = await _cardRespository.GetAllCards();

            return _mapper.Map<List<CardDTO>>(cards);

        }

        public async Task<ICollection<CardDTO>> GetAllCardsByList(int listId)
        {
            var cards = await _cardRespository.GetAllCardsByList(listId);

            return _mapper.Map<List<CardDTO>>(cards);
        }

        public async Task<CardDTO> GetCard(int cardId)
        {
            var cardExists = await _cardRespository.HasCard(cardId);
            if (cardExists)
            {
                return _mapper.Map<CardDTO>(await _cardRespository.GetCard(cardId));
            }
            else
                throw new NotImplementedException(); // TODO CARD NOT FOUND EXCEPTION
        }

        public async Task<CardDTO> UpdateCard(CardDTO updatedCard)
        {
            if (updatedCard == null)
                throw new NotImplementedException(); // TODO CARD BAD REQUEST EXCEPTION
            else
            {
                // TODO VALIDATE CARD CONTENTS
                var card = await _cardRespository.GetCard(updatedCard.Id);
                card.Title = updatedCard.Title;
                card.Description = updatedCard.Description;
                card.Status = updatedCard.Status;
                return _mapper.Map<CardDTO>(await _cardRespository.UpdateCard(card));
            }
        }

        public async Task AddAssignment(string username, int cardId)
        {
            await _assignmentService.AddAssignment(username, cardId);
        }

        public async Task RemoveAssignment(string username, int cardId)
        {
            await _assignmentService.RemoveAssignment(username, cardId);
        }
    }
}
