using Microsoft.AspNetCore.Http;
using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Services.Services.Interfaces;

public interface IMemoryService
{
    Task<User?> GetUserById(IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken);
}