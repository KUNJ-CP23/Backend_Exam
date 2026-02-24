using dotnet_backend.Data;
using dotnet_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_backend.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public AuthService(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public string GenerateJwtToken(Users user)
    {
        var jwtsettings = _configuration.GetSection("Jwt");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtsettings["Key"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.email)
        };

        var expiryMinutes = Convert.ToDouble(jwtsettings["ExpiryMinutes"]);
        
        var token = new JwtSecurityToken(
            issuer: jwtsettings["Issuer"],
            audience: jwtsettings["Audience"],
            expires: DateTime.Now.AddMinutes(expiryMinutes),
            claims: claims,
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}