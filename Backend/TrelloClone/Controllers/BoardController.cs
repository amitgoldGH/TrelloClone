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
    //    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("/display/{boardid}")]
        [ProducesResponseType(200, Type = typeof(BoardDisplayDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
        [Authorize]
        public async Task<IActionResult> GetDisplayBoard(int boardid)
        {

            var requestInitiator = GetCurrentUser();
            if (requestInitiator != null)
            {
                if (requestInitiator.BoardMemberships.Contains(boardid))
                {
                    return Ok(await _boardService.GetDisplayBoard(boardid));
                }
                else
                {
                    return Unauthorized("Not allowed to access this board.");
                }
            }
            return BadRequest();


        }

        [HttpGet("/display")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardDisplayDTO>))]
        public async Task<IActionResult> GetDisplayBoards()
        {
            var boards = await _boardService.GetAllDisplayBoards();
            return Ok(boards);
        }

        [HttpGet("{boardid}")]
        [ProducesResponseType(200, Type = typeof(BoardDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
        public async Task<IActionResult> GetBoard(int boardid)
        {
            var board = await _boardService.GetBoard(boardid);
            return Ok(board);
        }

        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardDTO>))]
        public async Task<IActionResult> GetBoards()
        {
            var boards = await _boardService.GetAllBoards();
            return Ok(boards);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(BoardDTO))]
        [TrelloControllerFilter]
        public async Task<IActionResult> CreateBoard(NewKanbanDTO input)
        {
            var board = await _boardService.CreateBoard(input);

            return Ok(board);
        }

        [HttpDelete("{boardid}")]
        [TrelloControllerFilter]
        public async Task<IActionResult> DeleteBoard(int boardid)
        {
            await _boardService.DeleteBoard(boardid);

            return Ok();
        }

        [HttpPut]
        [TrelloControllerFilter]
        public async Task<IActionResult> UpdateBoard(UpdateKanbanBoardDTO updatedBoard)
        {
            return Ok(await _boardService.UpdateBoard(updatedBoard));
        }

        [HttpPost("/membership")]
        [Consumes("application/json")]
        [TrelloControllerFilter]
        public async Task<IActionResult> AddMembership(NewMembershipDTO memDTO)
        {
            await _boardService.AddMember(memDTO.Username, memDTO.BoardId);

            return Ok();
        }

        [HttpDelete("/membership")]
        [Consumes("application/json")]
        [TrelloControllerFilter]
        public async Task<IActionResult> RemoveMembership(NewMembershipDTO memDTO)
        {
            await _boardService.RemoveMember(memDTO.Username, memDTO.BoardId);

            return Ok();
        }

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
