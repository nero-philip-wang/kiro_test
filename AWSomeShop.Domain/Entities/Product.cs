namespace AWSomeShop.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int PointsRequired { get; set; }
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}