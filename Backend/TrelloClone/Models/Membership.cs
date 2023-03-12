namespace TrelloClone.Models
{
    public class Membership
    {
        public int BoardId { get; set; }

        public string UserId { get; set; }

        public KanbanBoard KanbanBoard { get; set; }

        public User User { get; set; }
    }
}
