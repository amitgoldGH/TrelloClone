using AutoMapper;
using TrelloClone.DTO;
using TrelloClone.Models;

namespace TrelloClone.MapHelper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<KanbanBoard, KanbanBoardDTO>();

            CreateMap<Membership, UserMembershipDTO>().ForMember(dest => dest.BoardTitle, opt => opt.MapFrom(src => src.KanbanBoard.Title));

            CreateMap<Membership, BoardMembershipDTO>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
