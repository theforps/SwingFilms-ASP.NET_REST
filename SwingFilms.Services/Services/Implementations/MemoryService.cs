using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Services.Implementations;

public class MemoryService  : IMemoryService
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    
    public MemoryService(
        IUserRepository userRepository, 
        IMemoryCache memoryCache, 
        ISpaceRoomRepository spaceRoomRepository)
    {
        _userRepository = userRepository;
        _memoryCache = memoryCache;
        _spaceRoomRepository = spaceRoomRepository;
    }
    
    public async Task<User> GetCurrentUser(IHttpContextAccessor httpContextAccessor, CancellationToken cancellationToken)
    {
        var userIdString = httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!.Value;

        User? user = null;

        if (int.TryParse(userIdString, out var intUserId))
        {
            user = _memoryCache.Get<User>(userIdString) ?? await _userRepository.GetByTelegramId(intUserId, cancellationToken);
        }
        else if (Guid.TryParse(userIdString, out var intUserGuid))
        {
            user = _memoryCache.Get<User>(intUserGuid) ?? await _userRepository.GetById(intUserGuid, cancellationToken);
        }

        if (user != null)
        {
            _memoryCache.Set(Guid.Parse(userIdString), user);
        }

        return user;
    }

    public async Task<User?> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = _memoryCache.Get<User>(userId) 
                   ?? await _userRepository.GetById(userId, cancellationToken);
        
        _memoryCache.Set(userId, user);

        return user;
    }

    public async Task<SpaceRoom?> GetSpaceRoomById(Guid spaceRoomId, CancellationToken cancellationToken)
    {
        var room = _memoryCache.Get<SpaceRoom>(spaceRoomId) 
            ?? await _spaceRoomRepository.GetById(spaceRoomId, cancellationToken);
        
        _memoryCache.Set(spaceRoomId, room, TimeSpan.FromMinutes(15));

        return room;
    }
}