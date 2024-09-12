using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

public interface ITelegramUserRepository
{
    Task<TelegramUser?> GetUserByTelegramId(int id, CancellationToken cancellationToken);

    Task AddTelegramUser(TelegramUser telegramUser, CancellationToken cancellationToken);
}