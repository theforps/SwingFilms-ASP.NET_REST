using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SwingFilms.Services.Features.Identity.Commands;
using SwingFilms.Services.Features.Identity.Queries;
using SwingFilms.Services.Features.Room.Commands;
using SwingFilms.Services.Features.Room.Queries;
using SwingFilms.Services.Mapper;
using SwingFilms.Services.Services.Implementations;
using SwingFilms.Services.Services.Interfaces;


namespace SwingFilms.Services;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IMemoryService, MemoryService>();
        services.AddScoped<ISpaceRoomService, SpaceRoomService>();
    }
    
    public static void AddServiceLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Localization");
    }
    
    public static void AddServiceMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(UserMapper),
            typeof(RoomMapper)
        );
    }
    
    public static void AddServiceMediatR(this IServiceCollection services)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
    
    public static void AddServiceFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginSystemCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<RegistrationSystemCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<GetUserInfoQueryValidator>();
        
        services.AddValidatorsFromAssemblyContaining<ClearHistoryRoomCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<EditParameterRoomCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<DeleteRoomCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<EnterRoomCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<ExitRoomCommandValidator>();
        
        services.AddValidatorsFromAssemblyContaining<GetRoomMatchesQueryValidator>();
        services.AddValidatorsFromAssemblyContaining<GetRoomUserHistoryQueryValidator>();
        services.AddValidatorsFromAssemblyContaining<GetRoomQueryValidator>();
        services.AddValidatorsFromAssemblyContaining<GetRoomParameterQueryValidator>();
        services.AddValidatorsFromAssemblyContaining<GetRoomsQueryValidator>();
    }
        
    public static void AddServiceMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
    }
}