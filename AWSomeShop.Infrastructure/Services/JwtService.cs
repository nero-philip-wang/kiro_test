using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AWSomeShop.Domain.Interfaces;
using AWSomeShop.Domain.Options;

namespace AWSomeShop.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly Dictionary<int, List<RefreshTokenInfo>> _refreshTokens = new();

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateAccessToken(int userId, string email, int role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(int userId)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public (int? userId, string? email, int? role) ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Subject);
            var email = jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value;
            var role = int.Parse(jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value);

            return (userId, email, role);
        }
        catch
        {
            return (null, null, null);
        }
    }

    public (int? userId, string? email, int? role) ValidateRefreshToken(string token)
    {
        // For refresh tokens, we validate against stored tokens
        foreach (var userTokens in _refreshTokens)
        {
            var validToken = userTokens.Value.FirstOrDefault(t => 
                t.Token == token && t.Expiration > DateTime.UtcNow);
            
            if (validToken != null)
            {
                return (userTokens.Key, null, null);
            }
        }
        
        return (null, null, null);
    }

    public bool IsRefreshTokenValid(int userId, string token)
    {
        if (!_refreshTokens.TryGetValue(userId, out var tokens))
            return false;

        var validToken = tokens.FirstOrDefault(t => 
            t.Token == token && t.Expiration > DateTime.UtcNow);
        
        return validToken != null;
    }

    public void StoreRefreshToken(int userId, string token, DateTime expiration)
    {
        if (!_refreshTokens.ContainsKey(userId))
        {
            _refreshTokens[userId] = new List<RefreshTokenInfo>();
        }

        // Remove old expired tokens
        _refreshTokens[userId].RemoveAll(t => t.Expiration <= DateTime.UtcNow);

        _refreshTokens[userId].Add(new RefreshTokenInfo
        {
            Token = token,
            Expiration = expiration
        });
    }

    public void InvalidateRefreshToken(int userId, string token)
    {
        if (_refreshTokens.TryGetValue(userId, out var tokens))
        {
            tokens.RemoveAll(t => t.Token == token);
        }
    }

    public void InvalidateAllRefreshTokens(int userId)
    {
        if (_refreshTokens.ContainsKey(userId))
        {
            _refreshTokens[userId].Clear();
        }
    }

    private class RefreshTokenInfo
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}