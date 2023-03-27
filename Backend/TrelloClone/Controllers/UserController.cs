using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;
using TrelloClone.Services;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();

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
        [TrelloControllerFilter]
        public async Task<IActionResult> GetUser(string username)
        {


            var user = await _userService.GetUser(username);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);


        }


        [HttpPost("/register")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [TrelloControllerFilter]
        public async Task<IActionResult> RegisterUser(CredentialUserDTO newUser)
        {
            var createdUser = await _userService.CreateUser(newUser);
            if (createdUser == null)
                return BadRequest("Failed to create user.");
            else
                return Ok(createdUser);
        }


        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            await _userService.DeleteUser(username);

            return Ok();
        }

        [HttpPut("/update")]
        [TrelloControllerFilter]
        public async Task<IActionResult> UpdateUser(CredentialUserDTO updatedUser)
        {
            var user = await _userService.UpdateUser(updatedUser);

            return Ok(user);
        }

        /*[HttpGet("/TEST")]
        public async Task<IActionResult> TestFunction(string username)
        {
            var user = await _userService.TestFunction(username);

            return Ok(user);
        }*/
    }
}
