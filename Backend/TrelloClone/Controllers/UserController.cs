using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO;
using TrelloClone.Interfaces;
using TrelloClone.Models;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {


            var user = _userRepository.GetUser(username, _userRepository.HasUser);
            if (user == null)
            {
                return NotFound("No user found with given username.");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);


        }


        [HttpPost("/register")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        public IActionResult RegisterUser(CredentialUserDTO newUser)
        {
            var createdUser = _userRepository.CreateUser(newUser.Username, newUser.Password, _userRepository.HasUser);
            if (createdUser == null)
                return BadRequest("Failed to create user.");
            else
                return Ok(createdUser);
        }


        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            _userRepository.DeleteUser(username, _userRepository.HasUser);

            return Ok();
        }

        [HttpPut("/update")]
        public IActionResult UpdateUser(CredentialUserDTO updatedUser)
        {
            var user = _userRepository.UpdateUser(updatedUser, _userRepository.HasUser);

            return Ok(user);
        }
    }
}
