using Microsoft.EntityFrameworkCore;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;

namespace SwingFilms.Infrastructure.Repository.Implementations;

public class HistoryRoomRepository : IHistoryRoomRepository
{
    private readonly DataContext _dataContext;
    
    public HistoryRoomRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<History[]> GetUserHistoryInRoom(Guid userId, Guid roomId, CancellationToken cancellationToken)
    {
        var spaceRoom = await _dataContext.SpaceRooms
            .Include(spaceRoom => spaceRoom.Histories)
                .ThenInclude(history => history.Author)
            .FirstOrDefaultAsync(x => x.Id == roomId, cancellationToken);

        if (spaceRoom.Histories.Any())
        {
            var userHistory = spaceRoom.Histories
                .Where(x => x.Author.Id == userId);
            
            return userHistory.OrderByDescending(x => x.CreatedDate).ToArray();
        }

        return [];
    }

    public async Task<History[]> GetRoomMatches(Guid roomId, CancellationToken cancellationToken)
    {
        var spaceRoom = await _dataContext.SpaceRooms
            .Include(spaceRoom => spaceRoom.Histories)
                .ThenInclude(history => history.Author)
            .Include(spaceRoom => spaceRoom.Members)
            .FirstOrDefaultAsync(x => x.Id == roomId, cancellationToken);

        var roomHistory = spaceRoom.Histories
            .Where(x => x.IsWantToWatch)
            .ToArray();
        
        if (roomHistory.Any())
        {
            var roomMembersIds = spaceRoom.Histories
                .Select(history => history.Author.Id);

            var roomHistoryGrouped = roomHistory.GroupBy(history => history.FilmId);

            var filmMatches = new List<int>();
            
            foreach (var history in roomHistoryGrouped)
            {
                var usersLikedCurrentHistory = roomMembersIds
                    .Intersect(history.Select(x => x.Author.Id));
                
                if (usersLikedCurrentHistory.Count() == spaceRoom.Members.Count)
                    filmMatches.Add(history.Key);
            }
            
            return roomHistory
                .Where(x => filmMatches.Contains(x.FilmId))
                .ToArray();
        }
        
        return []; 
    }

    public async Task ClearRoomHistory(Guid roomId, CancellationToken cancellationToken)
    {
        var spaceRoom = await _dataContext.SpaceRooms
            .Include(x => x.Histories)
            .FirstOrDefaultAsync(x => x.Id == roomId, cancellationToken);
        
        spaceRoom.Histories.Clear();
        
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}