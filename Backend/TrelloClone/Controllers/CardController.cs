using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IAssignmentService _assignmentService;

        public CardController(ICardService cardService, IAssignmentService assignmentService)
        {
            _cardService = cardService;
            _assignmentService = assignmentService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardDTO>))]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _cardService.GetAllCards();

            return Ok(cards);
        }

        [HttpGet("{cardId}")]
        [ProducesResponseType(200, Type = typeof(CardDTO))]
        public async Task<IActionResult> GetCard(int cardId)
        {
            var card = await _cardService.GetCard(cardId);

            return Ok(card);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(200, Type = typeof(CardDTO))]
        public async Task<IActionResult> CreateCard(NewCardDTO newCard)
        {
            var card = await _cardService.CreateCard(newCard);

            return Ok(card);
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(int cardId)
        {
            await _cardService.DeleteCard(cardId);

            return Ok();
        }


        [HttpPost("{cardId}/assignment")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddAssignment(int cardId, [FromBody] string username)
        {
            Console.WriteLine(cardId + " " + username);

            await _assignmentService.AddAssignment(username, cardId);

            return Ok();
        }

        [HttpDelete("{cardId}/assignment")]
        [Consumes("application/json")]
        public  async Task<IActionResult> RemoveAssignment(int cardId, [FromBody] string username)
        {
            Console.WriteLine(cardId + " " + username);

            await _assignmentService.RemoveAssignment(username, cardId);

            return Ok();
        }
    }
}
