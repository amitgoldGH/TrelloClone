using TrelloClone.DTO;

namespace TrelloClone.Interfaces.Repositories
{
    public interface ICardRespository
    {
        Task<CardDTO> CreateCard(string title, string description, int boardListId);

        Task<CardDTO> UpdateCard(CardDTO updatedCard);

        Task DeleteCard(int cardId);

        Task<CardDTO> GetCard(int cardId);

        Task<ICollection<CardDTO>> GetAllCards();

        Task<ICollection<CardDTO>> GetAllCardsByList(int listId);
    }
}
