namespace TrelloClone.DTO.Display
{
    public class BoardListDTO
    {
        public int Id { get; set; } // Private key

        public string Title { get; set; }

        public int BoardId { get; set; }

        public ICollection<CardDTO> Cards { get; set; }
    }
}
