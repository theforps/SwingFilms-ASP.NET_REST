namespace SwingFilms.Services.Features.Room.DtoModels;

/// <summary>
/// DTO для параметрова комнаты
/// </summary>
public sealed record ParameterDto
{
    /// <summary>
    /// Жанр
    /// </summary>
    public string Genre { get; init; }
    
    /// <summary>
    /// Тип
    /// </summary>
    public string Type { get; init; }
    
    /// <summary>
    /// Минимальный рейтинг
    /// </summary>
    public int MinRate { get; init; }
    
    /// <summary>
    /// Максимальный рейтинг
    /// </summary>
    public int MaxRate { get; init; }
    
    /// <summary>
    /// Минимальный год
    /// </summary>
    public int MinYear { get; init; }
    
    /// <summary>
    /// Максимальный год
    /// </summary>
    public int MaxYear { get; init; }
}