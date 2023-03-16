using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO;
using TrelloClone.Interfaces;
using TrelloClone.Models;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;

        public BoardController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<KanbanBoardDTO>))]
        public IActionResult GetBoards()
        {
            var boards = _boardRepository.GetAllBoards();
            return Ok(boards);
        }
    }
}
