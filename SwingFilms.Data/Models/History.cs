using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class History
{
    [Key]
    public int Id { get; set; }
    
    public List<TelegramUser> AmateurUsers { get; set; } = new List<TelegramUser>();
    
    public int FilmId { get; set; }
}