using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
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
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {

            //var initiatingUser = GetCurrentUser();

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
            if (!Helper.Helper.illegalStringCheck(username))
            {
                var requestInitiator = GetCurrentUser();
                username = username.ToLower();
                if (requestInitiator.Role == "Admin" || requestInitiator.Username == username)
                {
                    var user = await _userService.GetUser(username);
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    return Ok(user);
                }
                else
                    return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }
            else
                return BadRequest(ExceptionMessages.UserBadRequest);




        }


        [HttpPost("/register")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [TrelloControllerFilter]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(CredentialUserDTO newUser)
        {
            if (newUser != null && !Helper.Helper.illegalStringCheck(newUser.Username) && !Helper.Helper.illegalStringCheck(newUser.Password))
            {
                newUser.Username = newUser.Username.ToLower();
                var createdUser = await _userService.CreateUser(newUser);
                if (createdUser == null)
                    return BadRequest("Failed to create user.");
                else
                    return Ok(createdUser);

            }
            else return BadRequest(ExceptionMessages.UserBadRequest);
        }


        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            if (Helper.Helper.illegalStringCheck(username))
            {
                return BadRequest("Illegal input");
            }
            var requestInitiatingUser = GetCurrentUser();
            if (requestInitiatingUser != null)
            {
                /*if (requestInitiatingUser.Username == username)
                {*/
                await _userService.DeleteUser(username);
                //}
                /*else
                {
                    return Unauthorized("Insufficient permissions to perform this action.");
                }*/
            }


            return Ok();
        }

        [HttpPut("/update")]
        [TrelloControllerFilter]
        public async Task<IActionResult> UpdateUser(CredentialUserDTO updatedUser)
        {
            var user = await _userService.UpdateUser(updatedUser);

            return Ok(user);
        }

        /*[HttpPost("/login")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(string))]
        [TrelloControllerFilter]
        public async Task<IActionResult> Login(CredentialUserDTO userLogin)
        {
            var token = await _userService.Login(userLogin);

            Console.WriteLine("TEST");
            return Ok(token);
        }
*/

        /*[HttpGet("/TEST")]
        public async Task<IActionResult> TestFunction(string username)
        {
            var user = await _userService.TestFunction(username);

            return Ok(user);
        }*/

        private RequestInitiatorDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new RequestInitiatorDTO
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };

                /*return new RequestInitiatorDTO
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    BoardMemberships = JsonConvert
                        .DeserializeObject<int[]>(userClaims
                                                    .FirstOrDefault(o => o.Type == Helper.Helper.authorizedBoardsClaimName)?.Value)

                };*/
            }
            return null;
        }
    }
}
