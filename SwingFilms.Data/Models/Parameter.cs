using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class Parameter
{
    [Key]
    public Guid Id { get; set; }
    
    public string? Genre { get; set; }
    
    public string? Year { get; set; }
    
    public string? Type { get; set; }
}