using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface IBoardRepository
    {
        bool HasBoard(int id);
        ICollection<KanbanBoard> GetAllBoards();

        ICollection<KanbanBoard> GetAllUserBoards(string username);

        KanbanBoard GetBoard(int id);

        KanbanBoard CreateBoard(string title, string username);

        KanbanBoard UpdateBoard(int id, string newTitle);
        void DeleteBoard(int id);


    }
}
