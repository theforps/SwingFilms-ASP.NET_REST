using SwingFilms.Infrastructure.Models;

namespace SwingFilms.Infrastructure.Repository.Interfaces;

public interface ISpaceRoomRepository : IBaseRepository<SpaceRoom>
{
    Task<SpaceRoom[]> GetAll(Guid userId, CancellationToken cancellationToken);

    Task UpdateParameter(Guid spaceRoomId, Parameter parameter, CancellationToken cancellationToken);

    Task RemoveMember(Guid spaceRoomId, Guid userId, CancellationToken cancellationToken);
}