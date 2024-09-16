using Microsoft.AspNetCore.Http;
using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Services.Services.Interfaces;

/// <summary>
/// Сервис обращения к кэшу
/// </summary>
public interface IMemoryService
{
    /// <summary>
    /// Получения пользователя
    /// </summary>
    /// <param name="httpContextAccessor">HTTP Контекст</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User?> GetUserById(IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken);
}