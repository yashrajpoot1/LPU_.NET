namespace InventoryManagementSystem;

/// <summary>
/// Electronics product with specific attributes
/// </summary>
public class Electronics : ProductBase
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int WarrantyPeriod { get; set; } // in months
    public double PowerUsage { get; set; } // in watts
    public DateTime ManufacturingDate { get; set; }

    public Electronics(
        string productId, 
        string name, 
        decimal price, 
        int stockQuantity, 
        string description,
        string brand,
        string model,
        int warrantyPeriod,
        double powerUsage,
        DateTime manufacturingDate)
        : base(productId, name, price, stockQuantity, description)
    {
        Brand = brand;
        Model = model;
        WarrantyPeriod = warrantyPeriod;
        PowerUsage = powerUsage;
        ManufacturingDate = manufacturingDate;
    }

    public override string GetCategory() => "Electronics";

    public override void DisplayDetails()
    {
        Console.WriteLine($"\n--- {GetCategory()} Product ---");
        Console.WriteLine($"ID: {ProductId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Brand: {Brand}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Price: ${Price:F2}");
        Console.WriteLine($"Stock: {StockQuantity} units");
        Console.WriteLine($"Warranty: {WarrantyPeriod} months");
        Console.WriteLine($"Power Usage: {PowerUsage}W");
        Console.WriteLine($"Manufacturing Date: {ManufacturingDate:yyyy-MM-dd}");
        Console.WriteLine($"Description: {Description}");
    }
}
