using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.Interfaces.Repositories
{
    public interface ICardRespository
    {
        Task<bool> HasCard(int cardId);

        Task<Card> CreateCard(string title, string description, int boardListId);

        Task<Card> UpdateCard(Card updatedCard);

        Task DeleteCard(int cardId);

        Task<Card> GetCard(int cardId);

        Task<ICollection<Card>> GetAllCards();

        Task<ICollection<Card>> GetAllCardsByList(int listId);
    }
}
