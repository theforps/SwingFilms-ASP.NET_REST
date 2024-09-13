namespace SwingFilms.Services.Features.Room.DtoModels;

public sealed record SpaceRoomDto
{
    public Guid Id { get; init; }
    
    public string Code { get; init; }
    
    public ParameterDto Parameters { get; init; }
    
    public Guid AdminId { get; init; }
    
    public Guid[] MembersIds { get; init; }
    
    public List<HistoryDto> Histories { get; set; }
}