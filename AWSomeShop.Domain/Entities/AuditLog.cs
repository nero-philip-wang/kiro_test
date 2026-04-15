namespace AWSomeShop.Domain.Entities;

public class AuditLog : BaseEntity
{
    public int? AdminUserId { get; set; }
    public int? TargetUserId { get; set; }
    public int? PointsChange { get; set; }
    public int? BalanceAfter { get; set; }
    public string? Reason { get; set; }

    public User? AdminUser { get; set; }
    public User? TargetUser { get; set; }
}