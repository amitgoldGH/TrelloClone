using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Services
{
    public class BoardListService : IBoardListService
    {
        private readonly IMapper _mapper;
        private readonly IBoardListRepository _boardListRepository;
        private readonly ICardService _cardService;

        public BoardListService(IMapper mapper, IBoardListRepository boardListRepository, ICardService cardService)
        {
            _mapper = mapper;
            _boardListRepository = boardListRepository;
            _cardService = cardService;
        }

        public async Task<BoardListDTO> CreateBoardList(NewBoardListDTO newBoardList)
        {
            var boardList = await _boardListRepository.CreateBoardList(newBoardList.Title, newBoardList.BoardId);
            return _mapper.Map<BoardListDTO>(boardList);
        }

        public async Task DeleteBoardList(int listId)
        {
            var listExists = await HasList(listId);
            if (listExists)
            {
                var bList = await _boardListRepository.GetSpecificList(listId);
                if (bList.Cards.Count > 0)
                {
                    foreach (Card card in bList.Cards)
                    {
                        await _cardService.DeleteCard(card.Id);
                    }
                }
                await _boardListRepository.DeleteBoardList(listId);
            }
        }

        public async Task<ICollection<BoardListDTO>> GetAllBoardLists()
        {
            var boardlists = await _boardListRepository.GetAllBoardLists();

            return _mapper.Map<List<BoardListDTO>>(boardlists);
        }

        public async Task<BoardListDTO> GetBoardList(int listId)
        {
            var boardlist = await _boardListRepository.GetSpecificList(listId);
            return _mapper.Map<BoardListDTO>(boardlist);
        }

        public async Task<ICollection<BoardListDTO>> GetSpecificBoardLists(int kanbanBoardId)
        {
            var boardlists = await _boardListRepository.GetSpecificBoardLists(kanbanBoardId);
            return _mapper.Map<List<BoardListDTO>>(boardlists);
        }

        public async Task<BoardListDTO> UpdateBoardList(BoardListDTO updatedList)
        {
            if (updatedList != null)
            {
                var boardList = await _boardListRepository.GetSpecificList(updatedList.Id);
                if (updatedList.Title != null)
                {
                    boardList.Title = updatedList.Title;
                }
                return _mapper.Map<BoardListDTO>(await _boardListRepository.UpdateBoardList(boardList));
            }
            else
                throw new NotImplementedException(); // BOARDLIST BAD REQUEST

        }

        public async Task<bool> HasList(int listId)
        {
            return await _boardListRepository.HasList(listId);
        }
    }
}
