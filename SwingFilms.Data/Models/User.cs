using System.ComponentModel.DataAnnotations;
using SwingFilms.Infrastructure.Enums;

namespace SwingFilms.Infrastructure.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public DateTime CreatedDate { get; } = DateTime.Now;
    
    public byte[] Image { get; set; }

    public bool IsActive { get; set; } = true;

    public UserRole Role { get; set; } = UserRole.User;
}