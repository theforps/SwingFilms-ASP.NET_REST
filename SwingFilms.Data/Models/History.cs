using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class History
{
    [Key]
    public Guid Id { get; set; }
    
    public User Author { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public int FilmId { get; set; }
    
    public bool IsWantToWatch { get; set; }
    
    public Guid SpaceRoomId { get; set; }
    
    public SpaceRoom SpaceRoom { get; set; }
}