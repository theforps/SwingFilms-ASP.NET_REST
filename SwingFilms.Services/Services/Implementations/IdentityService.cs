using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SwingFilms.Infrastructure.Enums;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Services.Implementations;

public class IdentityService : IIdentityService
{
    private readonly IConfiguration _configuration;
    
    public IdentityService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public bool VerifyPassword(string innerPassword, string storedHashPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(innerPassword, storedHashPassword);
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
    }

    public string CreateTokenJwt(UserRole userRole, Guid userId = default, int telegramUserId = default)
    {
        string userSid = userId != default ? userId.ToString() : telegramUserId.ToString();
        
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Sid, userSid),
            new Claim(ClaimTypes.Role, userRole.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("KeyTokenJWT").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}