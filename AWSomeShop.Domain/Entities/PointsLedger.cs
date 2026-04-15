namespace AWSomeShop.Domain.Entities;

public class PointsLedger : BaseEntity
{
    public int UserId { get; set; }
    public int Amount { get; set; }
    public int BalanceAfter { get; set; }
    public PointsLedgerType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime? ExpiredAt { get; set; }
    public bool IsExpired { get; set; } = false;
    public int? CreatedBy { get; set; }

    public User? User { get; set; }
}

public enum PointsLedgerType
{
    ManualGrant = 0,
    ManualDeduct = 1,
    Exchange = 2,
    Expire = 3
}