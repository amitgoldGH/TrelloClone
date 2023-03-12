namespace TrelloClone.Models
{
    public class Assignment
    {
        public int CardId { get; set; }

        public string UserId { get; set; }

        public Card Card { get; set; }

        public User User { get; set; }
    }
}
