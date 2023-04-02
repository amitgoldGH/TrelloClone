using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] CredentialUserDTO userLogin)
        {
            var user = await Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private string Generate(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            int[] authorizedBoardsArr;

            if (user.Memberships.Count == 0)
            {
                authorizedBoardsArr = new int[] { -1 };
            }
            else
            {
                authorizedBoardsArr = new int[user.Memberships.Count];
                for (int i = 0; i < user.Memberships.Count; i++)
                {
                    authorizedBoardsArr[i] = user.Memberships.ElementAt(i).BoardId;
                }
            }


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(Helper.Helper.authorizedBoardsClaimName, JsonConvert.SerializeObject(authorizedBoardsArr)),
            };


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserDTO> Authenticate(CredentialUserDTO userLogin)
        {
            var currentUser = await _userService.Login(userLogin);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

    }
}
