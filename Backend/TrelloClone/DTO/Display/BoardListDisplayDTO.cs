namespace TrelloClone.DTO.Display
{
    public class BoardListDisplayDTO
    {
        public int Id { get; set; } // Private key

        public string Title { get; set; }

        public ICollection<CardDisplayDTO> Cards { get; set; }
    }
}
