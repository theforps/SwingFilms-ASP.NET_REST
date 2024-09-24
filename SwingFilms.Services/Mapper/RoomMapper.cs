using AutoMapper;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Services.Features.Room.DtoModels;

namespace SwingFilms.Services.Mapper;

public class RoomMapper : Profile
{
    public RoomMapper()
    {
        CreateMap<SpaceRoom, SpaceRoomDto>()
            .ForMember(x => x.AdminId,
                options => options
                    .MapFrom(src => src.Admin.Id))
            .ForMember(x => x.Parameter,
                options => options
                    .MapFrom(src => src.Parameter))
            .ForMember(x => x.MembersIds,
                options => options
                    .MapFrom(src => src.Members.Select(x => x.Id).ToArray()));

        CreateMap<History, HistoryDto>();

        CreateMap<Parameter, EditParameterDto>().ReverseMap();
        
        CreateMap<Parameter, ParameterDto>().ReverseMap();
    }
}