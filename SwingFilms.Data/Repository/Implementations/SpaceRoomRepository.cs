using Microsoft.EntityFrameworkCore;
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

    public async Task<SpaceRoom?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dataContext.SpaceRooms
            .Include(x => x.Members)
            .Include(x => x.Admin)
            .Include(x => x.Parameter)
            .Include(x => x.Histories)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task Update(SpaceRoom model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task Add(SpaceRoom spaceRoom, CancellationToken cancellationToken)
    {
        await _dataContext.SpaceRooms.AddAsync(spaceRoom, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var spaceRoom = await GetById(id, cancellationToken);

        _dataContext.SpaceRooms.Remove(spaceRoom);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<SpaceRoom[]> GetAll(Guid userId, CancellationToken cancellationToken)
    {
        return await _dataContext.SpaceRooms
            .Include(x => x.Members)
            .Include(x => x.Admin)
            .Include(x => x.Parameter)
            .Where(x => x.Members.Select(user => user.Id == userId).Any())
            .ToArrayAsync(cancellationToken);
    }

    public async Task UpdateParameter(Guid spaceRoomId, Parameter parameter, CancellationToken cancellationToken)
    {
        var spaceRoom = await _dataContext.SpaceRooms
            .Include(spaceRoom => spaceRoom.Parameter)
            .FirstOrDefaultAsync(x => x.Id == spaceRoomId, cancellationToken);

        spaceRoom.Parameter.Genre = parameter.Genre;
        spaceRoom.Parameter.Type = parameter.Type;
        spaceRoom.Parameter.Year = parameter.Year;

        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Parameter> GetParameter(Guid spaceRoomId, CancellationToken cancellationToken)
    {
        var room = await _dataContext.SpaceRooms
            .Include(spaceRoom => spaceRoom.Parameter)
            .FirstOrDefaultAsync(x => x.Id == spaceRoomId, cancellationToken);
        
        return room.Parameter;
    }

    public async Task RemoveMember(Guid spaceRoomId, Guid userId, CancellationToken cancellationToken)
    {
        var spaceRoom = await _dataContext.SpaceRooms
            .Include(spaceRoom => spaceRoom.Members)
            .FirstOrDefaultAsync(x => x.Id == spaceRoomId, cancellationToken);
        
        var user = spaceRoom.Members.First(x => x.Id == userId);

        spaceRoom.Members.Remove(user);

        if (spaceRoom.Members.Count == 0)
            await Delete(spaceRoomId, cancellationToken);
        
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task EnterUserToRoom(string roomCode, Guid userId, CancellationToken cancellationToken)
    {
        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        
        var room = await _dataContext.SpaceRooms.FirstOrDefaultAsync(x => x.Code.Equals(roomCode), cancellationToken);
        
        room.Members.Add(user);
        
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<SpaceRoom> GetByCode(string spaceRoomCode, CancellationToken cancellationToken)
    {
        var room = await _dataContext.SpaceRooms.FirstOrDefaultAsync(x => x.Code.Equals(spaceRoomCode), cancellationToken);

        return room;
    }
}