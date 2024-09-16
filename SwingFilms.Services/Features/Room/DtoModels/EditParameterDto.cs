namespace SwingFilms.Services.Features.Room.DtoModels;

// TODO изменить после подобранного API
public sealed record EditParameterDto
{
    public Guid RoomId { get; init; }
    
    public string Genre { get; set; }
    
    public string Year { get; set; }
    
    public string Type { get; set; }
}