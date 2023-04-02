using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TrelloClone.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username { get; set; } // Private key

        public string Password { get; set; }

        public string Role { get; set; }

        public ICollection<Membership> Memberships { get; set; }

        public ICollection<Assignment> Assignments { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
