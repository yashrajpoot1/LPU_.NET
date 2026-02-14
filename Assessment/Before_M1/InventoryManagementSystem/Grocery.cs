namespace InventoryManagementSystem;

/// <summary>
/// Grocery product with specific attributes
/// </summary>
public class Grocery : ProductBase
{
    public DateTime ExpiryDate { get; set; }
    public double Weight { get; set; } // in kg
    public bool IsOrganic { get; set; }
    public double StorageTemperature { get; set; } // in Celsius

    public Grocery(
        string productId,
        string name,
        decimal price,
        int stockQuantity,
        string description,
        DateTime expiryDate,
        double weight,
        bool isOrganic,
        double storageTemperature)
        : base(productId, name, price, stockQuantity, description)
    {
        ExpiryDate = expiryDate;
        Weight = weight;
        IsOrganic = isOrganic;
        StorageTemperature = storageTemperature;
    }

    public override string GetCategory() => "Grocery";

    public bool IsExpired() => DateTime.Now > ExpiryDate;

    public override void DisplayDetails()
    {
        Console.WriteLine($"\n--- {GetCategory()} Product ---");
        Console.WriteLine($"ID: {ProductId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Price: ${Price:F2}");
        Console.WriteLine($"Stock: {StockQuantity} units");
        Console.WriteLine($"Weight: {Weight}kg");
        Console.WriteLine($"Organic: {(IsOrganic ? "Yes" : "No")}");
        Console.WriteLine($"Storage Temperature: {StorageTemperature}°C");
        Console.WriteLine($"Expiry Date: {ExpiryDate:yyyy-MM-dd}");
        Console.WriteLine($"Status: {(IsExpired() ? "EXPIRED" : "Fresh")}");
        Console.WriteLine($"Description: {Description}");
    }
}
