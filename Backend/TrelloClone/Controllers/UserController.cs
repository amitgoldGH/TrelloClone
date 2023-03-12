using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string username)
        {
            if (!_userRepository.HasUser(username))
                return NotFound("User not found");
            else
            {
                var user = _userRepository.GetUser(username); 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(user);
            }

        }
    }
}
