namespace AWSomeShop.Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int PointsSpent { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? TrackingNumber { get; set; }
    public int? AddressId { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? ShippedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public User? User { get; set; }
    public Product? Product { get; set; }
    public Address? Address { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    Shipped = 1,
    Completed = 2,
    NoShipping = 3
}