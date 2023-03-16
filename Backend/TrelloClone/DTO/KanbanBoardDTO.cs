
namespace TrelloClone.DTO
{

    public class KanbanBoardDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<BoardMembershipDTO> Members { get; set; }

        public KanbanBoardDTO()
        {

        }
    }
}
