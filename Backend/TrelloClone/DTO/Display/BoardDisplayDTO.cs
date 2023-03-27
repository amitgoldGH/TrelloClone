namespace TrelloClone.DTO.Display
{
    public class BoardDisplayDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<BoardMembershipDTO> Members { get; set; }

        public ICollection<BoardListDisplayDTO> Lists { get; set; }
    }
}
