using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

/// <summary>
/// Работа с комнатами
/// </summary>
public interface ISpaceRoomRepository : IBaseRepository<SpaceRoom>
{
    /// <summary>
    /// Получение всех комнат пользователя
    /// </summary>
    /// <param name="userId">Идентификатор комнаты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Массив комнат</returns>
    Task<SpaceRoom[]> GetAll(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление параметров комнаты
    /// </summary>
    /// <param name="spaceRoomId">Идентификатор комнаты</param>
    /// <param name="parameter">Параметр</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task UpdateParameter(Guid spaceRoomId, Parameter parameter, CancellationToken cancellationToken);

    /// <summary>
    /// Получение параметров комнаты
    /// </summary>
    /// <param name="spaceRoomId">Идентификатор комнаты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Параметры комнаты</returns>
    Task<Parameter> GetParameter(Guid spaceRoomId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаление пользователя из комнаты
    /// </summary>
    /// <param name="spaceRoomId">Идентификатор комнаты</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task RemoveMember(Guid spaceRoomId, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление пользователя в комнату
    /// </summary>
    /// <param name="spaceRoomCode">Код комнаты</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task EnterUserToRoom(string spaceRoomCode, Guid userId, CancellationToken cancellationToken);
}