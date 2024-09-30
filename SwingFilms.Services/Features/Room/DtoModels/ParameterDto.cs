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
    /// Год
    /// </summary>
    public string Year { get; init; }
    
    /// <summary>
    /// Тип фильма
    /// </summary>
    public string Type { get; init; }
}