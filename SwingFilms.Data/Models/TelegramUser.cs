using System.ComponentModel.DataAnnotations;
using SwingFilms.Infrastructure.Enums;

namespace SwingFilms.Infrastructure.Models;

public class TelegramUser
{
    [Key]
    public int Id { get; set; }
    
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    public bool IsActive { get; set; } = true;

    public UserRole Role { get; set; } = UserRole.User;
}