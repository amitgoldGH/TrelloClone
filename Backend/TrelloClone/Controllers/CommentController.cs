/*using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();

            return Ok(comments);
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(200, Type = typeof(CommentDTO))]
        public async Task<IActionResult> GetComment(int commentId)
        {
            var comment = await _commentService.GetComment(commentId);

            return Ok(comment);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(CommentDTO))]
        public async Task<IActionResult> CreateComment(NewCommentDTO newComment)
        {
            var comment = await _commentService.CreateComment(newComment);

            return Ok(comment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            await _commentService.DeleteComment(commentId);

            return Ok();
        }

    }
}
*/