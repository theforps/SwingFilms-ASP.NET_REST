using Microsoft.EntityFrameworkCore;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;

namespace SwingFilms.Infrastructure.Repository.Implementations;

public class UserRepository(DataContext dataContext) : IUserRepository
{
    public async Task<User?> GetByLogin(string login, CancellationToken cancellationToken)
    {
        return await dataContext.Users.FirstOrDefaultAsync(x => x.Login.Equals(login), cancellationToken);
    }

    public async Task<bool> IsActiveUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return user.IsActive;
    }

    public async Task ChangeUserActive(Guid id, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        user.IsActive = !user.IsActive;

        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return user;
    }

    public async Task Update(User model, CancellationToken cancellationToken)
    {
        dataContext.Update(model);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Add(User model, CancellationToken cancellationToken)
    {
        await dataContext.Users.AddAsync(model, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        dataContext.Users.Remove(user);
        await dataContext.SaveChangesAsync(cancellationToken);
    }
}