using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

/// <summary>
/// Модель фильтров комнаты
/// </summary>
public class Parameter
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Жанр
    /// </summary>
    public string? Genre { get; set; }
    
    /// <summary>
    /// Тип
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// Минимальный рейтинг
    /// </summary>
    public int MinRate { get; set; }
    
    /// <summary>
    /// Максимальный рейтинг
    /// </summary>
    public int MaxRate { get; set; }
    
    /// <summary>
    /// Минимальный год
    /// </summary>
    public int MinYear { get; set; }
    
    /// <summary>
    /// Максимальный год
    /// </summary>
    public int MaxYear { get; set; }
    
    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public Guid SpaceRoomId { get; set; }
    
    /// <summary>
    /// Комната
    /// </summary>
    public SpaceRoom SpaceRoom { get; set; }
}