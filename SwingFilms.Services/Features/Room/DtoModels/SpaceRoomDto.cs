namespace SwingFilms.Services.Features.Room.DtoModels;

/// <summary>
/// DTO комнаты
/// </summary>
public sealed record SpaceRoomDto
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Код комнаты
    /// </summary>
    public string Code { get; init; }
    
    /// <summary>
    /// DTO параметров комнаты
    /// </summary>
    public ParameterDto Parameter { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор администраторы комнаты
    /// </summary>
    public Guid AdminId { get; init; }
    
    /// <summary>
    /// Уникальные идентификаторы участников комнаты
    /// </summary>
    public Guid[] MembersIds { get; init; }
}