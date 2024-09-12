using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SwingFilms.Services.Features.Identity.Commands;
using SwingFilms.Services.Mapper;
using SwingFilms.Services.Services.Implementations;
using SwingFilms.Services.Services.Interfaces;


namespace SwingFilms.Services;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
    }
    
    public static void AddServiceLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Localization");
    }
    
    public static void AddServiceMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(UserMapper)
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
    }
    
        
    public static void AddServiceMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
    }
}