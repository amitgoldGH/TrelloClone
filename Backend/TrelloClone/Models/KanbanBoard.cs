namespace TrelloClone.Models
{
    public class KanbanBoard
    {
        public int Id { get; set; } // Private key

        public string Title { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public ICollection<BoardList> BoardLists { get; set; }
    }
}
