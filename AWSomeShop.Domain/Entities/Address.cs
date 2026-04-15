namespace AWSomeShop.Domain.Entities;

public class Address : BaseEntity
{
    public int UserId { get; set; }
    public string RecipientName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string DetailAddress { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;

    public User? User { get; set; }
}