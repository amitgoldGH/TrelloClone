using TrelloClone.Models;

namespace TrelloClone.Interfaces
{
    public interface IBoardRepository
    {
        bool HasBoard(int boardid);
        ICollection<KanbanBoard> GetAllBoards();

        ICollection<KanbanBoard> GetAllUserBoards(string username);

        KanbanBoard GetBoard(int boardid);

        KanbanBoard AddMember(int boardid, string username);

        KanbanBoard CreateBoard(string title, string username);

        KanbanBoard UpdateBoard(int boardid, KanbanBoard newBoard);
        void DeleteBoard(int boardid);


    }
}
