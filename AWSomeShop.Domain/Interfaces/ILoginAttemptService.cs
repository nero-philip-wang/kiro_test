namespace AWSomeShop.Domain.Interfaces;

public interface ILoginAttemptService
{
    void RecordFailedAttempt(string email);
    void RecordSuccess(string email);
    bool IsLockedOut(string email);
    TimeSpan? GetRemainingLockoutTime(string email);
    int GetFailedAttempts(string email);
    void ClearAttempts(string email);
}