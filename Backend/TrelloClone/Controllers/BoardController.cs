using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet("{boardid}")]
        [ProducesResponseType(200, Type = typeof(KanbanBoardShortDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
        public async Task<IActionResult> GetBoard(int boardid)
        {
            var board = await _boardService.GetBoard(boardid);
            return Ok(board);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<KanbanBoardShortDTO>))]
        public async Task<IActionResult> GetBoards()
        {
            var boards = await _boardService.GetAllBoards();
            return Ok(boards);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(KanbanBoardShortDTO))]
        [TrelloControllerFilter]
        public async Task<IActionResult> CreateBoard(NewKanbanDTO input)
        {
            var board = await _boardService.CreateBoard(input.Title, input.UserId);

            return Ok(board);
        }

        [HttpDelete("{boardid}")]
        [TrelloControllerFilter]
        public async Task<IActionResult> DeleteBoard(int boardid)
        {
            await _boardService.DeleteBoard(boardid);

            return Ok();
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
    }
}
