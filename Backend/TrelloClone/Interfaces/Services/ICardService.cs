using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;

namespace TrelloClone.Interfaces.Services
{
    public interface ICardService
    {
        Task<CardDTO> CreateCard(NewCardDTO newCard);

        Task<CardDTO> UpdateCard(CardDTO updatedCard);

        Task DeleteCard(int cardId);

        Task<CardDTO> GetCard(int cardId);

        Task<ICollection<CardDTO>> GetAllCards();

        Task<ICollection<CardDTO>> GetAllCardsByList(int listId);
    }
}
