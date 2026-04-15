namespace AWSomeShop.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;
    public bool IsActive { get; set; } = true;
    public string Language { get; set; } = "zh-CN";
    public DateTime? LastLoginAt { get; set; }

    public ICollection<PointsLedger> PointsLedgers { get; set; } = new List<PointsLedger>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}

public enum UserRole
{
    Employee = 0,
    Admin = 1,
    SuperAdmin = 2
}