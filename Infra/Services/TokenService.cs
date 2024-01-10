using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Configuration;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Services;
public class TokenService : ITokenService
{
    private readonly JwtConfig _config;
    public TokenService(IOptions<JwtConfig> config)
    {
        _config = config.Value;
    }
   
    public string GenerateToken(ApplicationUser applicationUser)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: GenerateClaims(applicationUser),
            expires: DateTime.UtcNow.AddMinutes(_config.ExpirationMinutes),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private IEnumerable<Claim> GenerateClaims(ApplicationUser applicationUser)
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Expiration, TimeSpan.FromMinutes(_config.ExpirationMinutes).Ticks.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, applicationUser.Email));
        return claims;
    }
}