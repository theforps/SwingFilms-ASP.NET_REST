namespace SwingFilms.Services.Features.Room.DtoModels;

// TODO изменить после подобранного API
/// <summary>
/// DTO для редактирования параметров комнаты
/// </summary>
public sealed record EditParameterDto
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