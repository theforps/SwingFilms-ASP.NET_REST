namespace SwingFilms.Infrastructure.Repository.Interfaces;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T">Обрабатываемая сущность</typeparam>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Получение по Id
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность</returns>
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="model">Сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task Update(T model, CancellationToken cancellationToken);

    /// <summary>
    /// Добавление сущности
    /// </summary>
    /// <param name="model">Сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task Add(T model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task Delete(Guid id, CancellationToken cancellationToken);
}