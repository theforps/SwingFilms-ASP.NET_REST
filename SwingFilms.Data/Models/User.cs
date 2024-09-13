using System.ComponentModel.DataAnnotations;
using SwingFilms.Infrastructure.Enums;

namespace SwingFilms.Infrastructure.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public int? TelegramId { get; set; }
    
    public string? Login { get; set; }
    
    public string? Password { get; set; }
    
    public DateTime CreatedDate { get; } = DateTime.Now;
    
    public byte[]? Image { get; set; }

    public bool IsActive { get; set; } = true;

    public UserRole Role { get; set; } = UserRole.User;

    public List<SpaceRoom> SpaceRooms { get; set; } = new();
    
    public List<SpaceRoom> AdminSpaceRooms { get; set; } = new();

    public List<History> Histories { get; set; } = new();
}