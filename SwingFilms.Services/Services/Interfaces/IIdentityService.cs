using Microsoft.AspNetCore.Http;
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
    /// <returns>Результат кодировки</returns>
    string HashPassword(string password);

    /// <summary>
    /// Создание JWT токена
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    /// <param name="userRole">Роль пользователя</param>
    /// <param name="telegramUserId">Telegram User Id</param>
    /// <returns>Созданный JWT токен</returns>
    string CreateTokenJwt(UserRole userRole, Guid userId = default, int telegramUserId = default);

    /// <summary>
    /// Конвертация IFormFile в массив байтов
    /// </summary>
    /// <param name="file">Загружаемый файл</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Массив байтов</returns>
    Task<byte[]> ConvertFormFileToByteArray(IFormFile file, CancellationToken cancellationToken);
}