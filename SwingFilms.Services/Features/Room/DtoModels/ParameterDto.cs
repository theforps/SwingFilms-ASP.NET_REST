namespace SwingFilms.Services.Features.Room.DtoModels;

public sealed record ParameterDto
{
    public string Genre { get; init; }
    
    public string Year { get; init; }
    
    public string Type { get; init; }
}