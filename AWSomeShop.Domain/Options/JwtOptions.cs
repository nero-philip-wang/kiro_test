namespace AWSomeShop.Domain.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 120; // 2 hours
    public int RefreshTokenExpirationDays { get; set; } = 30; // 30 days
}