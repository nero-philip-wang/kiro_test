namespace AWSomeShop.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? NameEn { get; set; }
    public int SortOrder { get; set; } = 0;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}