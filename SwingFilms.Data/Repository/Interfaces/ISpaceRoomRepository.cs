using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

public interface ISpaceRoomRepository
{
    Task AddSpaceRoom(SpaceRoom spaceRoom, CancellationToken cancellationToken);
}