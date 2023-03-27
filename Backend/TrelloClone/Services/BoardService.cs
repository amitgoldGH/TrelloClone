using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.DTO.Update;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Services
{
    public class BoardService : IBoardService
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;
        private readonly IMembershipService _membershipService;
        private readonly IUserService _userService;

        public BoardService(IMapper mapper, IBoardRepository boardRepository, IMembershipService membershipService, IUserService userService)
        {
            _mapper = mapper;
            _boardRepository = boardRepository;
            _membershipService = membershipService;
            _userService = userService;
        }
        public async Task AddMember(string username, int boardid)
        {
            var userExists = await _userService.HasUser(username);
            if (userExists)
            {
                var boardExists = await HasBoard(boardid);
                if (boardExists)
                {
                    await _membershipService.AddMembership(username, boardid);
                }
                else
                {
                    throw new BoardNotFoundException();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }

        }

        public async Task RemoveMember(string username, int boardid)
        {
            var userExists = await _userService.HasUser(username);
            if (userExists)
            {
                var boardExists = await HasBoard(boardid);
                if (boardExists)
                {
                    await _membershipService.RemoveMembership(username, boardid);
                }
                else
                {
                    throw new BoardNotFoundException();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public async Task AddMembers(string[] usernames, int boardid)
        {
            if (usernames != null && usernames.Length > 0)
            {
                var boardExists = await HasBoard(boardid);
                if (boardExists)
                {
                    foreach (string username in usernames)
                    {
                        var userExists = await _userService.HasUser(username);

                        if (userExists)
                        {
                            await _membershipService.AddMembership(username, boardid);
                        }
                    }
                }
            }
        }

        public async Task RemoveMembers(string[] usernames, int boardid)
        {
            if (usernames != null && usernames.Length > 0)
            {
                var boardExists = await HasBoard(boardid);
                if (boardExists)
                {
                    foreach (string username in usernames)
                    {
                        var userExists = await _userService.HasUser(username);

                        if (userExists)
                        {
                            await _membershipService.RemoveMembership(username, boardid);
                        }
                    }
                }
            }
        }

        public async Task<BoardDTO> CreateBoard(NewKanbanDTO newBoard)
        {
            if (newBoard == null)
            {
                throw new BoardBadRequestException();
            }
            else
            {
                var userExists = await _userService.HasUser(newBoard.UserId);
                if (userExists)
                {
                    var board = await _boardRepository.CreateBoard(newBoard.Title);
                    await AddMember(newBoard.UserId, board.Id);
                    return _mapper.Map<BoardDTO>(board);
                }
                else
                {
                    throw new BoardBadRequestException();
                }
            }
        }

        public async Task DeleteBoard(int boardid)
        {

            var board = await GetBoard(boardid);
            if (board.Members.Count > 0)
            {
                foreach (BoardMembershipDTO mem in board.Members)
                {
                    await RemoveMember(mem.Username, board.Id);
                }
            }
            await _boardRepository.DeleteBoard(board.Id);
        }

        public async Task<ICollection<BoardDTO>> GetAllBoards()
        {
            var boards = await _boardRepository.GetAllBoards();
            return _mapper.Map<List<BoardDTO>>(boards);
        }

        public async Task<BoardDTO> GetBoard(int boardid)
        {
            var boardExists = await HasBoard(boardid);

            if (boardExists)
                return _mapper.Map<BoardDTO>(await _boardRepository.GetBoard(boardid));
            else
                throw new BoardNotFoundException();


        }

        public async Task<ICollection<BoardDisplayDTO>> GetAllDisplayBoards()
        {
            var boards = await _boardRepository.GetAllBoards();
            return _mapper.Map<List<BoardDisplayDTO>>(boards);
        }
        public async Task<BoardDisplayDTO> GetDisplayBoard(int boardid)
        {
            var boardExists = await HasBoard(boardid);
            if (boardExists)
                return _mapper.Map<BoardDisplayDTO>(await _boardRepository.GetBoard(boardid));
            else
                throw new BoardNotFoundException();
        }
        public Task<bool> HasBoard(int boardid)
        {
            return _boardRepository.HasBoard(boardid);
        }

        public async Task<BoardDTO> UpdateBoard(UpdateKanbanBoardDTO updatedBoard)
        {
            var boardExists = await HasBoard(updatedBoard.Id);
            if (boardExists)
            {
                var board = await _boardRepository.GetBoard(updatedBoard.Id);
                if (updatedBoard.NewTitle != null)
                    board.Title = updatedBoard.NewTitle;
                return _mapper.Map<BoardDTO>(await _boardRepository.UpdateBoard(board));

            }
            else
                throw new BoardNotFoundException();
        }
    }
}
