namespace SwingFilms.Services.Features.Identity.DtoModels;

public sealed record UserInfoDto
{
    public Guid Id { get; init; }
    
    public int TelegramId { get; init; }
    
    public string Login { get; init; }
    
    public DateOnly CreatedDate { get; init; }
    

    public bool IsActive { get; init; } 

    public string Role { get; init; }
    
    public byte[] Image { get; init; }
}