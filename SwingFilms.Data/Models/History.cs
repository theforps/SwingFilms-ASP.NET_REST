using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class History
{
    [Key]
    public Guid Id { get; set; }
    
    public List<User> AmateurUsers { get; set; } = new List<User>();
    
    public int FilmId { get; set; }
}