namespace TrelloClone.Models
{
    public class Comment
    {
        public int Id { get; set; } // Private key

        public string Text { get; set; }

        public Card Card { get; set; }

        public User Author { get; set; }
    }
}
