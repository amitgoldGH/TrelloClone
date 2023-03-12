namespace TrelloClone.Models
{
    public class Card
    {
        public int Id { get; set; } // Private key

        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public BoardList BoardList { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

    }
}
