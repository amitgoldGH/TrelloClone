using AutoMapper;
using TrelloClone.DTO.Creation;
using TrelloClone.DTO.Display;
using TrelloClone.Models;

namespace TrelloClone.MapHelper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<Comment, CommentDTO>();

            CreateMap<Card, CardDTO>().ForMember(dest => dest.AssignmentList, opt => opt.MapFrom(src => src.Assignments));
            CreateMap<Card, CardDisplayDTO>();

            CreateMap<BoardList, BoardListDTO>();
            CreateMap<BoardList, BoardListDisplayDTO>();

            CreateMap<Assignment, CardAssignmentDTO>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<KanbanBoard, BoardDTO>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Memberships))
                .ForMember(dest => dest.Lists, opt => opt.MapFrom(src => src.BoardLists));
            CreateMap<KanbanBoard, BoardDisplayDTO>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Memberships))
                .ForMember(dest => dest.Lists, opt => opt.MapFrom(src => src.BoardLists));

            CreateMap<Membership, UserMembershipDTO>().ForMember(dest => dest.BoardTitle, opt => opt.MapFrom(src => src.KanbanBoard.Title));

            CreateMap<Membership, BoardMembershipDTO>().ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
