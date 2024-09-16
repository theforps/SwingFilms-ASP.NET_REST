using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

public interface IHistoryRoomRepository
{
    Task<History[]?> GetUserHistoryInRoom(Guid userId, Guid roomId, CancellationToken cancellationToken);
    
    Task<History[]> GetRoomMatches(Guid roomId, CancellationToken cancellationToken);
    
    Task ClearRoomHistory(Guid roomId, CancellationToken cancellationToken);
}