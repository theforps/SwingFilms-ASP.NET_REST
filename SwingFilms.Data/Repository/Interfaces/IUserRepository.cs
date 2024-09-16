using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

/// <summary>
/// Работа с пользователем
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Получение пользователя по логину
    /// </summary>
    /// <param name="login">Логин</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User?> GetByLogin(string login, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение пользователя по идентификатору telegram 
    /// </summary>
    /// <param name="telegramId">Telegram идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь</returns>
    Task<User?> GetByTelegramId(int telegramId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение активности пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Логический тип</returns>
    Task<bool> IsActiveUser(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Изменение активности пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task ChangeUserActive(Guid userId, CancellationToken cancellationToken);
}