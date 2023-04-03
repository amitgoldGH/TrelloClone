using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.DTO.Update;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        ///////////////////////
        // PRIVATE FUNCTIONS //
        ///////////////////////

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

        private async Task<bool> CheckCurrentUserBoardAccess(int boardId)
        {
            var requestInitiator = GetCurrentUser();
            if (requestInitiator == null)
                return false;
            else
            {
                var actionAllowed = await _boardService.CheckUserActionAllowed(requestInitiator, boardId);
                return actionAllowed;
            }
        }

        ///////////////////
        // BOARD ACTIONS //
        ///////////////////

        [HttpGet("/display/{boardId}")]
        [ProducesResponseType(200, Type = typeof(BoardDisplayDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
        //[Authorize]
        public async Task<IActionResult> GetDisplayBoard(int boardId)
        {

            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                return Ok(await _boardService.GetDisplayBoard(boardId));
            }
            else
            {
                return Unauthorized("Not allowed to access this board.");
            }



        }

        [HttpGet("/display")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardDisplayDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetDisplayBoards()
        {
            var boards = await _boardService.GetAllDisplayBoards();
            return Ok(boards);
        }

        [HttpGet("{boardId}")]
        [ProducesResponseType(200, Type = typeof(BoardDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
        //[Authorize]
        public async Task<IActionResult> GetBoard(int boardId)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                var board = await _boardService.GetBoard(boardId);
                return Ok(board);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetBoards()
        {
            var boards = await _boardService.GetAllBoards();
            return Ok(boards);
        }

        [HttpPost("{Title}")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(BoardDTO))]
        [TrelloControllerFilter]
        //[Authorize]
        public async Task<IActionResult> CreateBoard(string Title)
        {
            var requestInitiator = GetCurrentUser();
            NewKanbanDTO input = new NewKanbanDTO { Title = Title, UserId = requestInitiator.Username };
            var board = await _boardService.CreateBoard(input);

            return Ok(board);
        }

        [HttpDelete("{boardId}")]
        [TrelloControllerFilter]
        //[Authorize]
        public async Task<IActionResult> DeleteBoard(int boardId)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                await _boardService.DeleteBoard(boardId);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }

        }

        [HttpPut("{boardId}")]
        [Consumes("application/json")]
        [TrelloControllerFilter]
        //[Authorize]
        public async Task<IActionResult> UpdateBoard(int boardId, [FromBody] UpdateKanbanBoardDTO updatedBoard)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                return Ok(await _boardService.UpdateBoard(updatedBoard));
            }
            else
            {
                return Unauthorized();
            }
        }


        ////////////////////////
        // MEMBERSHIP ACTIONS //
        ////////////////////////

        [HttpPost("{boardId}/membership/{username}")]
        [TrelloControllerFilter]
        //  [Authorize]
        public async Task<IActionResult> AddMembership(int boardId, string username)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                await _boardService.AddMember(username, boardId);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{boardId}/membership/{username}")]
        [TrelloControllerFilter]
        //[Authorize]
        public async Task<IActionResult> RemoveMembership(int boardId, string username)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                await _boardService.RemoveMember(username, boardId);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }


        }


    }
}
