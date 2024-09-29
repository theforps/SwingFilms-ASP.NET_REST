using Microsoft.AspNetCore.Http;

namespace SwingFilms.Services.Features.Identity.DtoModels;

public sealed record RegistrationUserDto
{
    public string Login { get; init; }
    
    public string Password { get; init; }
    
    public IFormFile? Image { get; init; }
    
    public string Role { get; init; }
}