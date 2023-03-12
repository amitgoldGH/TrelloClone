namespace TrelloClone.Models
{
    public class BoardList
    {
        public int Id { get; set; } // Private key

        public string Title { get; set; }

        public KanbanBoard KanbanBoard { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
