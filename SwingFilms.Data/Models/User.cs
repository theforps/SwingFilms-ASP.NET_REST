using System.ComponentModel.DataAnnotations;
using SwingFilms.Infrastructure.Enums;

namespace SwingFilms.Infrastructure.Models;

/// <summary>
/// Модель пользователя
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Telegram ID
    /// </summary>
    public int? TelegramId { get; set; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string? Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime CreatedDate { get; } = DateTime.Now;
    
    /// <summary>
    /// Изображение профиля
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// Пользователь активен
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public UserRole Role { get; set; } = UserRole.User;

    /// <summary>
    /// Комнаты
    /// </summary>
    public List<SpaceRoom> SpaceRooms { get; set; } = new();
    
    /// <summary>
    /// Комнаты, где администратор
    /// </summary>
    public List<SpaceRoom> AdminSpaceRooms { get; set; } = new();

    /// <summary>
    /// Истории
    /// </summary>
    public List<History> Histories { get; set; } = new();
}