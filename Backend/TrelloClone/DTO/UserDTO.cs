using TrelloClone.Models;

namespace TrelloClone.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } // Private key

        public ICollection<MembershipDTO> Memberships { get; set; }


        public UserDTO()
        {

        }
        public UserDTO(string username, ICollection<MembershipDTO> memberships)
        {
            Username = username;
            Memberships = memberships;
        }
    }
}
