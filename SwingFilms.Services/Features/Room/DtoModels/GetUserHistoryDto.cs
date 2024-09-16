namespace SwingFilms.Services.Features.Room.DtoModels;

public sealed record GetUserHistoryDto
{
    public Guid RoomId { get; init; }
}