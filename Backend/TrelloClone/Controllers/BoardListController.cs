/*using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardListController : Controller
    {
        private readonly IBoardListService _boardListService;

        public BoardListController(IBoardListService boardListService)
        {
            _boardListService = boardListService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardListDTO>))]
        public async Task<IActionResult> GetAllLists()
        {
            var lists = await _boardListService.GetAllBoardLists();
            return Ok(lists);
        }

        [HttpGet("{listId}")]
        [ProducesResponseType(200, Type = typeof(BoardListDTO))]
        public async Task<IActionResult> GetList(int listId)
        {
            var list = await _boardListService.GetBoardList(listId);
            return Ok(list);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(BoardListDTO))]
        public async Task<IActionResult> CreateList(NewBoardListDTO newBoardList)
        {

            var list = await _boardListService.CreateBoardList(newBoardList);
            return Ok(list);
        }

        [HttpDelete("{listId}")]
        public async Task<IActionResult> DeleteList(int listId)
        {
            await _boardListService.DeleteBoardList(listId);
            return Ok();
        }
    }
}
*/