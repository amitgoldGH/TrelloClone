using AutoMapper;
using TrelloClone.Data;
using TrelloClone.DTO;
using TrelloClone.Interfaces.Repositories;

namespace TrelloClone.Repository
{
    public class BoardListRepository : IBoardListRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BoardListRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<BoardListDTO> CreateBoardList(string title)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBoardList(int listId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BoardListDTO>> GetAllBoardLists()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<BoardListDTO>> GetSpecificBoardLists(int kanbanBoardId)
        {
            throw new NotImplementedException();
        }

        public Task<BoardListDTO> GetSpecificList(int listId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasList(int listId)
        {
            throw new NotImplementedException();
        }

        public Task<BoardListDTO> UpdateBoardList(int listId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
