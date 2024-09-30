using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Infrastructure.Models;

/// <summary>
/// Модель история просмотра
/// </summary>
public class History
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Автор
    /// </summary>
    public User Author { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Идентификатор фильма
    /// </summary>
    public int FilmId { get; set; }
    
    /// <summary>
    /// Желание посмотреть
    /// </summary>
    public bool IsWantToWatch { get; set; }
    
    public Guid SpaceRoomId { get; set; }
    
    /// <summary>
    /// Комната
    /// </summary>
    public SpaceRoom SpaceRoom { get; set; }
}