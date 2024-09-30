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
    /// Жанр фильма
    /// </summary>
    public string? Genre { get; set; }
    
    /// <summary>
    /// Год фильма
    /// </summary>
    public string? Year { get; set; }
    
    /// <summary>
    /// Тип фильма
    /// </summary>
    public string? Type { get; set; }
    
    public Guid SpaceRoomId { get; set; }
    
    /// <summary>
    /// Комната
    /// </summary>
    public SpaceRoom SpaceRoom { get; set; }
}