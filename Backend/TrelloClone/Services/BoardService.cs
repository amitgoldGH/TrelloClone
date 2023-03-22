using TrelloClone.DTO;
using TrelloClone.Exceptions;
using TrelloClone.Interfaces.Repositories;
using TrelloClone.Interfaces.Services;
using TrelloClone.Models;

namespace TrelloClone.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMembershipService _membershipService;
        private readonly IUserService _userService;

        public BoardService(IBoardRepository boardRepository, IMembershipService membershipService, IUserService userService)
        {
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

        public async Task<KanbanBoardShortDTO> CreateBoard(string title, string username)
        {
            var userExists = await _userService.HasUser(username);
            if (userExists)
            {
                var board = await _boardRepository.CreateBoard(title, username);
                await AddMember(username, board.Id);
                return board;
            }
            else
            {
                throw new BoardBadRequestException();
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

        public async Task<ICollection<KanbanBoardShortDTO>> GetAllBoards()
        {
            var boards = await _boardRepository.GetAllBoards();
            return boards;
        }

        public async Task<KanbanBoardShortDTO> GetBoard(int boardid)
        {
            var boardExists = await HasBoard(boardid);

            if (boardExists)
                return await _boardRepository.GetBoard(boardid);
            else
                throw new BoardNotFoundException();


        }

        public Task<bool> HasBoard(int boardid)
        {
            return _boardRepository.HasBoard(boardid);
        }

        public async Task<KanbanBoardShortDTO> UpdateBoard(KanbanBoardShortDTO newBoard)
        {
            var boardExists = await HasBoard(newBoard.Id);

            if (boardExists)
            {
                return await _boardRepository.UpdateBoard(newBoard.Id, newBoard.Title);
            }
            else
                throw new BoardNotFoundException();
        }
    }
}
