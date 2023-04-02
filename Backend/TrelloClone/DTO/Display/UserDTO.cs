using TrelloClone.Models;

namespace TrelloClone.DTO.Display
{
    public class UserDTO
    {
        public string Username { get; set; } // Private key

        public string Role { get; set; }

        public ICollection<UserMembershipDTO> Memberships { get; set; }


        public UserDTO()
        {

        }
        public UserDTO(string username, ICollection<UserMembershipDTO> memberships)
        {
            Username = username;
            Memberships = memberships;
        }
    }
}
