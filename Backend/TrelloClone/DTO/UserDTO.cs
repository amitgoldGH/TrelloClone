using TrelloClone.Models;

namespace TrelloClone.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } // Private key

        public ICollection<Membership> Memberships { get; set; }

        public UserDTO(string username, ICollection<Membership> memberships)
        {
            Username = username;
            Memberships = memberships;
        }
    }
}
