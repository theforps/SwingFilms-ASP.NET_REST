namespace SwingFilms.Services.Features.Identity.DtoModels;

/// <summary>
/// DTO для входа в систему
/// </summary>
public sealed record LoginUserDto
{
    /// <summary>
    /// TelegramId
    /// </summary>
    public int TelegramId { get; init; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; init; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { get; init; }
}