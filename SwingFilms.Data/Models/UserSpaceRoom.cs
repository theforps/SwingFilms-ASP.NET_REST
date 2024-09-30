namespace SwingFilms.Infrastructure.Models;

/// <summary>
/// Отношение пользователя к комнате
/// </summary>
public class UserSpaceRoom
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    public Guid SpaceRoomId { get; set; }
}