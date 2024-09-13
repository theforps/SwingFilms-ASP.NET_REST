using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByLogin(string login, CancellationToken cancellationToken);
    
    Task<User?> GetByTelegramId(int telegramId, CancellationToken cancellationToken);

    Task<bool> IsActiveUser(Guid id, CancellationToken cancellationToken);

    Task ChangeUserActive(Guid id, CancellationToken cancellationToken);
}