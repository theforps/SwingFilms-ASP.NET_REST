using Microsoft.EntityFrameworkCore;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;

namespace SwingFilms.Infrastructure.Repository.Implementations;

public class TelegramUserRepository : ITelegramUserRepository
{
    private readonly DataContext _dataContext;
    
    public TelegramUserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<TelegramUser?> GetUserByTelegramId(int id, CancellationToken cancellationToken)
    {
        return await _dataContext.TelegramUsers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddTelegramUser(TelegramUser telegramUser, CancellationToken cancellationToken)
    {
        await _dataContext.TelegramUsers.AddAsync(telegramUser, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}