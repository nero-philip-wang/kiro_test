namespace AWSomeShop.Domain.Entities;

public class SystemConfig : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public new DateTime? UpdatedAt { get; set; }
}