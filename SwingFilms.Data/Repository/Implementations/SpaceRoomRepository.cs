using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;

namespace SwingFilms.Infrastructure.Repository.Implementations;

public class SpaceRoomRepository : ISpaceRoomRepository
{
    private readonly DataContext _dataContext;
    
    public SpaceRoomRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task AddSpaceRoom(SpaceRoom spaceRoom, CancellationToken cancellationToken)
    {
        await _dataContext.SpaceRooms.AddAsync(spaceRoom, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}