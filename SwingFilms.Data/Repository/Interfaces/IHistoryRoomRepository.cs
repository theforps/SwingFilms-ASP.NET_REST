using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

/// <summary>
/// Работа с историями в комнате
/// </summary>
public interface IHistoryRoomRepository
{
    /// <summary>
    /// Получение истории фильмов пользователя в комнате
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Массив историй</returns>
    Task<History[]> GetUserHistoryInRoom(Guid userId, Guid roomId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получения совпадений по фильмам в комнате
    /// </summary>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Массив историй</returns>
    Task<History[]> GetRoomMatches(Guid roomId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Очистка истории в комнате
    /// </summary>
    /// <param name="roomId">Идентификатор комнаты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Массив историй</returns>
    Task ClearRoomHistory(Guid roomId, CancellationToken cancellationToken);
}