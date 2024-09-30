using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

/// <summary>
/// Модель комнаты
/// </summary>
public class SpaceRoom
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Код комнаты
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Фильтры комнаты
    /// </summary>
    public Parameter Parameter { get; set; } = new();
    
    /// <summary>
    /// Админ
    /// </summary>
    public User Admin { get; set; }
    
    /// <summary>
    /// Участники комнаты
    /// </summary>
    public List<User> Members { get; set; } = new();
    
    /// <summary>
    /// История комнаты
    /// </summary>
    public List<History> Histories { get; set; } = new();
}