namespace AWSomeShop.Domain.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(int userId, string email, int role);
    string GenerateRefreshToken(int userId);
    (int? userId, string? email, int? role) ValidateToken(string token);
    (int? userId, string? email, int? role) ValidateRefreshToken(string token);
    bool IsRefreshTokenValid(int userId, string token);
    void StoreRefreshToken(int userId, string token, DateTime expiration);
    void InvalidateRefreshToken(int userId, string token);
    void InvalidateAllRefreshTokens(int userId);
}