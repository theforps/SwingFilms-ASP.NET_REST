namespace SwingFilms.Services.Features.Room.DtoModels;

/// <summary>
/// DTO для просмотра истории
/// </summary>
public sealed record HistoryDto
{
    // public Guid[] AmateurUsersIds { get; init; }
    //
    // public Guid[] IgnoredUsersIds { get; init; }
    
    /// <summary>
    /// Идентификатор фильма
    /// </summary>
    public int FilmId { get; init; }
}