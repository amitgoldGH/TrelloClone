namespace TrelloClone.DTO
{
    public class CardDTO
    {
        public int Id { get; set; } // Private key

        public string Title { get; set; }

        public string Description { get; set; }

        public int BoardListId { get; set; }

        public int Status { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}
