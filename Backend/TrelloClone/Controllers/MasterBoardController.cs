using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.DTO.Update;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Controllers
{
    [Route("api/Board")]
    [ApiController]
    [Authorize]
    public class MasterBoardController : Controller
    {
        private readonly IBoardService _boardService;
        private readonly IBoardListService _boardListService;
        private readonly ICardService _cardService;
        private readonly IAssignmentService _assignmentService;
        private readonly ICommentService _commentService;

        public MasterBoardController(IBoardService boardService,
                IBoardListService boardListService,
                ICardService cardService,
                IAssignmentService assignmentService,
                ICommentService commentService)
        {
            _boardService = boardService;
            _boardListService = boardListService;
            _cardService = cardService;
            _assignmentService = assignmentService;
            _commentService = commentService;
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

        private async Task<bool> CheckChildListAccess(int boardId, int listId)
        {
            var list = await _boardListService.GetBoardList(listId);

            if (list != null && list.BoardId == boardId)
            {
                return await CheckCurrentUserBoardAccess(boardId);
            }
            else
                return false;
        }

        private async Task<bool> CheckChildCardAccess(int boardId, int listId, int cardId)
        {
            var card = await _cardService.GetCard(cardId);
            if (card != null && card.BoardListId == listId)
            {
                return await CheckChildListAccess(boardId, listId);
            }
            else
                return false;
        }

        private async Task<bool> CheckChildCommentAccess(int boardId, int listId, int cardId, int commentId)
        {
            var comment = await _commentService.GetComment(commentId);

            if (comment != null && comment.CardId == cardId)
            {
                return await CheckChildCardAccess(boardId, listId, cardId);
            }
            else
                return false;
        }

        /////////////////////
        /// BOARD ACTIONS ///
        /////////////////////

        [HttpGet("display/{boardId}")]
        [ProducesResponseType(200, Type = typeof(BoardDisplayDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
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

        [HttpGet("display")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardDisplayDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetDisplayBoards()
        {
            var boards = await _boardService.GetAllDisplayBoards();
            return Ok(boards);
        }

        /*[HttpGet("{boardId}")]
        [ProducesResponseType(200, Type = typeof(BoardDTO))]
        [ProducesResponseType(404)]
        [TrelloControllerFilter]
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
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }
        }
*/
        /*[HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetBoards()
        {
            var boards = await _boardService.GetAllBoards();
            return Ok(boards);
        }*/

        [HttpPost("{Title}")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(BoardDTO))]
        [TrelloControllerFilter]
        public async Task<IActionResult> CreateBoard(string Title)
        {
            var requestInitiator = GetCurrentUser();
            NewKanbanDTO input = new() { Title = Title, UserId = requestInitiator.Username };
            var board = await _boardService.CreateBoard(input);

            return Ok(board);
        }

        [HttpDelete("{boardId}")]
        [TrelloControllerFilter]
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
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }

        }

        [HttpPut("{boardId}")]
        [Consumes("application/json")]
        [TrelloControllerFilter]
        public async Task<IActionResult> UpdateBoard(int boardId, [FromBody] UpdateKanbanBoardDTO updatedBoard)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                return Ok(await _boardService.UpdateBoard(updatedBoard));
            }
            else
            {
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }
        }


        ////////////////////////
        // MEMBERSHIP ACTIONS //
        ////////////////////////

        [HttpPost("{boardId}/membership/{username}")]
        [TrelloControllerFilter]
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
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
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
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }


        }

        //////////////////////////
        /// BOARD LIST ACTIONS ///
        //////////////////////////

        [HttpGet("/test/listTest")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardListDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLists()
        {
            var lists = await _boardListService.GetAllBoardLists();
            return Ok(lists);
        }

        [HttpGet("{boardId}/list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoardListDTO>))]
        public async Task<IActionResult> GetListsOfBoard(int boardId)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                var lists = await _boardListService.GetSpecificBoardLists(boardId);
                return Ok(lists);
            }
            else
            {
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }
        }

        [HttpGet("{boardId}/list/{listId}")]
        [ProducesResponseType(200, Type = typeof(BoardListDTO))]
        public async Task<IActionResult> GetList(int boardId, int listId)
        {
            var accessAllowed = await CheckChildListAccess(boardId, listId);
            if (accessAllowed)
            {
                var list = await _boardListService.GetBoardList(listId);
                return Ok(list);

            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }

        [HttpPost("{boardId}/list/{title}")]
        [ProducesResponseType(200, Type = typeof(BoardListDTO))]
        public async Task<IActionResult> CreateList(int boardId, string title)
        {
            var accessAllowed = await CheckCurrentUserBoardAccess(boardId);
            if (accessAllowed)
            {
                var list = await _boardListService.CreateBoardList(new NewBoardListDTO { BoardId = boardId, Title = title });
                return Ok(list);
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }

        [HttpDelete("{boardId}/list/{listId}")]
        public async Task<IActionResult> DeleteList(int boardId, int listId)
        {
            var accessAllowed = await CheckChildListAccess(boardId, listId);
            if (accessAllowed)
            {
                await _boardListService.DeleteBoardList(listId);
                return Ok();
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }


        ////////////////////
        /// CARD ACTIONS ///
        ////////////////////

        [HttpGet("/test/cardTest")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardDTO>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardService.GetAllCards();

            return Ok(cards);
        }

        [HttpGet("{boardId}/list/{listId}/card/{cardId}")]
        [ProducesResponseType(200, Type = typeof(CardDTO))]
        public async Task<IActionResult> GetCard(int boardId, int listId, int cardId)
        {
            var accessAllowed = await CheckChildCardAccess(boardId, listId, cardId);
            if (accessAllowed)
            {
                var card = await _cardService.GetCard(cardId);
                return Ok(card);
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }

        [HttpPost("{boardId}/list/{listId}/card")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(CardDTO))]
        public async Task<IActionResult> CreateCard(int boardId, int listId, [FromBody] NewCardDTO newCard)
        {
            if (newCard != null)
            {
                var accessAllowed = await CheckChildListAccess(boardId, listId);
                if (accessAllowed)
                {
                    var card = await _cardService.CreateCard(newCard, listId);
                    return Ok(card);
                }
                else
                    return Unauthorized(ExceptionMessages.UnauthorizedAction);
            }
            else return BadRequest();


        }

        [HttpDelete("{boardId}/list/{listId}/card/{cardId}")]
        public async Task<IActionResult> DeleteCard(int boardId, int listId, int cardId)
        {
            var accessAllowed = await CheckChildCardAccess(boardId, listId, cardId);

            if (accessAllowed)
            {
                await _cardService.DeleteCard(cardId);
                return Ok();
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }


        [HttpPost("{boardId}/list/{listId}/card/{cardId}/assignment/{username}")]
        public async Task<IActionResult> AddAssignment(int boardId, int listId, int cardId, string username)
        {
            Console.WriteLine("Add Assignment " + cardId + " " + username);
            var accessAllowed = await CheckChildCardAccess(boardId, listId, cardId);
            if (accessAllowed)
            {
                await _assignmentService.AddAssignment(username, cardId);
                return Ok();
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);

        }

        [HttpDelete("{boardId}/list/{listId}/card/{cardId}/assignment/{username}")]
        public async Task<IActionResult> RemoveAssignment(int boardId, int listId, int cardId, string username)
        {
            Console.WriteLine("Remove Assignment " + cardId + " " + username);
            var accessAllowed = await CheckChildCardAccess(boardId, listId, cardId);
            if (accessAllowed)
            {
                await _assignmentService.RemoveAssignment(username, cardId);
                return Ok();
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }


        ///////////////////////
        /// COMMENT ACTIONS ///
        ///////////////////////

        [HttpGet("/test/commentTest")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();

            return Ok(comments);
        }

        [HttpGet("{boardId}/list/{listId}/card/{cardId}/comment")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetCommentsOfCard(int boardId, int listId, int cardId)
        {
            var accessAllowed = await CheckChildCardAccess(boardId, listId, cardId);

            if (accessAllowed)
            {
                var comments = await _commentService.GetAllCardComments(cardId);
                return Ok(comments);
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }

        [HttpGet("{boardId}/list/{listId}/card/{cardId}/comment/{commentId}")]
        [ProducesResponseType(200, Type = typeof(CommentDTO))]
        public async Task<IActionResult> GetComment(int boardId, int listId, int cardId, int commentId)
        {
            var accessAllowed = await CheckChildCommentAccess(boardId, listId, cardId, commentId);

            if (accessAllowed)
            {
                var comment = await _commentService.GetComment(commentId);
                return Ok(comment);
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);
        }

        [HttpPost("{boardId}/list/{listId}/card/{cardId}/comment")]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(CommentDTO))]
        public async Task<IActionResult> CreateComment(int boardId, int listId, int cardId, [FromBody] NewCommentDTO newComment)
        {
            var accessAllowed = await CheckChildCardAccess(boardId, listId, cardId);
            if (accessAllowed)
            {
                var requestInitiator = GetCurrentUser();
                var comment = await _commentService.CreateComment(newComment, requestInitiator.Username, cardId);
                return Ok(comment);
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);

        }

        [HttpDelete("{boardId}/list/{listId}/card/{cardId}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int boardId, int listId, int cardId, int commentId)
        {
            var accessAllowed = await CheckChildCommentAccess(boardId, listId, cardId, commentId);
            if (accessAllowed)
            {
                await _commentService.DeleteComment(commentId);
                return Ok();
            }
            else
                return Unauthorized(ExceptionMessages.UnauthorizedAction);

        }


    }
}
