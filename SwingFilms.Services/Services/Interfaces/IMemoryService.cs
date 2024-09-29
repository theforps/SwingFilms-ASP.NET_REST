using Microsoft.AspNetCore.Http;
using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Services.Services.Interfaces;

/// <summary>
/// Сервис обращения к кэшу
/// </summary>
public interface IMemoryService
{
    /// <summary>
    /// Получения текущего пользователя
    /// </summary>
    /// <param name="httpContextAccessor">HTTP Контекст</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User> GetCurrentUser(IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение комнаты по идентификатору
    /// </summary>
    /// <param name="spaceRoomId">Идентификатор комнаты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Комната</returns>
    Task<SpaceRoom?> GetSpaceRoomById(Guid spaceRoomId, CancellationToken cancellationToken);
}