namespace TrelloClone.DTO.Creation
{
    public class CredentialUserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public CredentialUserDTO()
        {

        }

        public CredentialUserDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
