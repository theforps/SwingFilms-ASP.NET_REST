namespace SwingFilms.Services.Features.Identity.DtoModels;

/// <summary>
/// DTO для предоставление информации о пользователе
/// </summary>
public sealed record UserInfoDto
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Telegram ID
    /// </summary>
    public int TelegramId { get; init; }
    
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; init; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateOnly CreatedDate { get; init; }
    
    /// <summary>
    /// Активность пользователя
    /// </summary>
    public bool IsActive { get; init; } 

    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string Role { get; init; }
    
    /// <summary>
    /// Изображение профиля
    /// </summary>
    public byte[] Image { get; init; }
}