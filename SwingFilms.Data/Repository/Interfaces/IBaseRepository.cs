namespace SwingFilms.Infrastructure.Repository.Interfaces;

public interface IBaseRepository<T>
{
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);

    Task Update(T model, CancellationToken cancellationToken);

    Task Add(T model, CancellationToken cancellationToken);

    Task Delete(Guid id, CancellationToken cancellationToken);
}