using Microsoft.EntityFrameworkCore;
using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TelegramUser> TelegramUsers { get; set; }
    public DbSet<SpaceRoom> SpaceRooms { get; set; }
    public DbSet<History> Histories { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
}