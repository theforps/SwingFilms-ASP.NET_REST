using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SwingFilms.Infrastructure.Repository.Implementations;
using SwingFilms.Infrastructure.Repository.Interfaces;

namespace SwingFilms.Infrastructure;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
    }

    public static void AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
    }
}