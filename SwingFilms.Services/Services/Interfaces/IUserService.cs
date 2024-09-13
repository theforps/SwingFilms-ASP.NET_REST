using Microsoft.AspNetCore.Http;
using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Services.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserId(IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken);
}