using Microsoft.AspNetCore.Http;

namespace SwingFilms.Services.Features.Identity.DtoModels;

/// <summary>
/// DTO для регистрации в системе
/// </summary>
public sealed record RegistrationUserDto
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; init; }
    
    /// <summary>
    /// Изображение профиля
    /// </summary>
    public IFormFile? Image { get; init; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string Role { get; init; }
}