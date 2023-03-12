using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface IBoardListRepository
    {
        ICollection<BoardList> GetAllBoardLists();

        ICollection<BoardList> GetSpecificBoardLists(int kanbanBoardId);

        BoardList GetSpecificList(int listId);

        BoardList CreateBoardList(string title);

        BoardList UpdateBoardList(int listId, string newTitle);

        void DeleteBoardList(int listId);

        bool HasList(int listId);
    }
}
