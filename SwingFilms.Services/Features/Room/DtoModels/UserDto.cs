namespace SwingFilms.Services.Features.Room.DtoModels;

public sealed record UserDto
{
    public Guid Id { get; init; }
    
    public string? Login { get; init; }
    
    public string Role { get; init; }
    
    public bool IsActive { get; init; }
}