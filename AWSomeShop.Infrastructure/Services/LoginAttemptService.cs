using AWSomeShop.Domain.Interfaces;

namespace AWSomeShop.Infrastructure.Services;

public class LoginAttemptService : ILoginAttemptService
{
    private readonly Dictionary<string, LoginAttemptInfo> _attempts = new();
    private const int MaxFailedAttempts = 5;
    private static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(10);

    public void RecordFailedAttempt(string email)
    {
        var normalizedEmail = email.ToLowerInvariant();
        
        if (!_attempts.ContainsKey(normalizedEmail))
        {
            _attempts[normalizedEmail] = new LoginAttemptInfo();
        }

        _attempts[normalizedEmail].FailedAttempts++;
        _attempts[normalizedEmail].LastFailedAttempt = DateTime.UtcNow;

        if (_attempts[normalizedEmail].FailedAttempts >= MaxFailedAttempts)
        {
            _attempts[normalizedEmail].LockoutEnd = DateTime.UtcNow.Add(LockoutDuration);
        }
    }

    public void RecordSuccess(string email)
    {
        var normalizedEmail = email.ToLowerInvariant();
        ClearAttempts(normalizedEmail);
    }

    public bool IsLockedOut(string email)
    {
        var normalizedEmail = email.ToLowerInvariant();
        
        if (!_attempts.TryGetValue(normalizedEmail, out var info))
        {
            return false;
        }

        if (info.LockoutEnd.HasValue && info.LockoutEnd > DateTime.UtcNow)
        {
            return true;
        }

        // Reset lockout if expired
        if (info.LockoutEnd.HasValue && info.LockoutEnd <= DateTime.UtcNow)
        {
            info.FailedAttempts = 0;
            info.LockoutEnd = null;
        }

        return false;
    }

    public TimeSpan? GetRemainingLockoutTime(string email)
    {
        var normalizedEmail = email.ToLowerInvariant();
        
        if (!_attempts.TryGetValue(normalizedEmail, out var info) || !info.LockoutEnd.HasValue)
        {
            return null;
        }

        var remaining = info.LockoutEnd.Value - DateTime.UtcNow;
        return remaining > TimeSpan.Zero ? remaining : null;
    }

    public int GetFailedAttempts(string email)
    {
        var normalizedEmail = email.ToLowerInvariant();
        
        if (!_attempts.TryGetValue(normalizedEmail, out var info))
        {
            return 0;
        }

        return info.FailedAttempts;
    }

    public void ClearAttempts(string email)
    {
        var normalizedEmail = email.ToLowerInvariant();
        _attempts.Remove(normalizedEmail);
    }

    private class LoginAttemptInfo
    {
        public int FailedAttempts { get; set; }
        public DateTime LastFailedAttempt { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}