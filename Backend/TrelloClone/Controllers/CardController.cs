using Microsoft.AspNetCore.Mvc;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
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
        public IActionResult AddAssignment(int cardId, [FromBody] string username)
        {
            Console.WriteLine(cardId + " " + username);
            return Ok();
        }

        [HttpDelete("{cardId}/assignment")]
        [Consumes("application/json")]
        public IActionResult RemoveAssignment(int cardId, [FromBody] string username)
        {
            Console.WriteLine(cardId + " " + username);

            return Ok();
        }
    }
}
