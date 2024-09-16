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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(x => x.Histories)
            .WithOne(e => e.Author);
        
        modelBuilder.Entity<User>()
            .HasMany(x => x.SpaceRooms)
            .WithMany(e => e.Members)
            .UsingEntity<UserSpaceRoom>();

        modelBuilder.Entity<SpaceRoom>()
            .HasOne(x => x.Admin)
            .WithMany(x => x.AdminSpaceRooms);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<SpaceRoom> SpaceRooms { get; set; }
}