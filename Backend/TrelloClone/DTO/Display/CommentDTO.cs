namespace TrelloClone.DTO.Display
{
    public class CommentDTO
    {
        public int Id { get; set; }

        public int CardId { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }
    }
}
