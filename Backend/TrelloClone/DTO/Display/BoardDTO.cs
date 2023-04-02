namespace TrelloClone.DTO.Display
{

    public class BoardDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<BoardMembershipDTO> Members { get; set; }

        public ICollection<BoardListDTO> Lists { get; set; }
        public BoardDTO()
        {

        }
    }
}
