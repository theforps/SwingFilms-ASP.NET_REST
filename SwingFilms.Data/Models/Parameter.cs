using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class Parameter
{
    [Key]
    public int Id { get; set; }
}