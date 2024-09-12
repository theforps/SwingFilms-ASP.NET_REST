using SwingFilms.Infrastructure.Enums;

namespace SwingFilms.Services.Services.Interfaces;

/// <summary>
/// Сервис входа в систему
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// Проверка пароля пользователя
    /// </summary>
    /// <param name="innerPassword">Входящий пароль</param>
    /// <param name="storedHashPassword">Хэшированный пароль из БД</param>
    /// <returns>Результат проверка</returns>
    bool VerifyPassword(string innerPassword, string storedHashPassword);

    /// <summary>
    /// Кодировка пароля
    /// </summary>
    /// <param name="password">Входящий пароль</param>
    /// <returns>Результат кодировка</returns>
    string HashPassword(string password);

    /// <summary>
    /// Создание JWT токена
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    /// <param name="userRole">Роль пользователя</param>
    /// <returns>Созданный JWT токен</returns>
    string CreateTokenJwt(UserRole userRole, Guid userId = default, int telegramUserId = default);
}