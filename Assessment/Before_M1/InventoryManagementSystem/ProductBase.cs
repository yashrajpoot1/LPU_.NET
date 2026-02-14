namespace InventoryManagementSystem;

/// <summary>
/// Base class for all products with common properties
/// </summary>
public abstract class ProductBase
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; }

    protected ProductBase(string productId, string name, decimal price, int stockQuantity, string description)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        Description = description;
    }

    public abstract string GetCategory();
    public abstract void DisplayDetails();
}
