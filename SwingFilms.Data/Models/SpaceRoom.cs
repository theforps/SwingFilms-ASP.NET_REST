using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

public class SpaceRoom
{
    [Key]
    public int Id { get; set; }
    
    public string Code { get; set; }
    
    public Parameter Parameter { get; set; } = new Parameter();
    
    public TelegramUser Admin { get; set; }
    
    public List<TelegramUser> Members { get; set; } = new List<TelegramUser>();
    
    public History History { get; set; } = new History();
}