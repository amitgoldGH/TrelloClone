using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;

namespace TrelloClone.Services
{
    public class BoardListService : IBoardListService
    {
        private readonly IMapper _mapper;
        private readonly IBoardListRepository _boardListRepository;

        public BoardListService(IMapper mapper, IBoardListRepository boardListRepository)
        {
            _mapper = mapper;
            _boardListRepository = boardListRepository;
        }

        public async Task<BoardListDTO> CreateBoardList(NewBoardListDTO newBoardList)
        {
            var boardList = await _boardListRepository.CreateBoardList(newBoardList.Title, newBoardList.BoardId);
            return _mapper.Map<BoardListDTO>(boardList);
        }

        public async Task DeleteBoardList(int listId)
        {
            await _boardListRepository.DeleteBoardList(listId);
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
    }
}
