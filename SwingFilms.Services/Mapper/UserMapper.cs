using AutoMapper;
using SwingFilms.Infrastructure.Enums;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Services.Features.Identity.DtoModels;

namespace SwingFilms.Services.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserInfoDto>()
            .ForMember(x => x.Role, 
                options => options
                    .MapFrom(src => src.Role.ToString()))
            .ForMember(x => x.CreatedDate,
                options => options
                    .MapFrom(src => DateOnly.FromDateTime(src.CreatedDate)));
        
        CreateMap<RegistrationUserDto, User>()
            .ForMember(x => x.Role,
                options => options
                    .MapFrom(src => src.Role.Equals("User") ? UserRole.User : UserRole.Admin))
            .ForMember(x => x.Image, 
                options => options.Ignore());
    }
}