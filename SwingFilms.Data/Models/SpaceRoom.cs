using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class SpaceRoom
{
    [Key]
    public Guid Id { get; set; }
    
    public string Code { get; set; }
    
    public Parameter? Parameter { get; set; }
    
    public User Admin { get; set; }
    
    public List<User> Members { get; set; } = new();
    
    public List<History>? Histories { get; set; }
}