using System.ComponentModel.DataAnnotations;

namespace SwingFilms.Services.Features.Identity.DtoModels;

/// <summary>
/// DTO для входа в систему
/// </summary>
public sealed record LoginUserDto
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required]
    public string Login { get; init; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required]
    public string Password { get; init; }
}