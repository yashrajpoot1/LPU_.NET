namespace InventoryManagementSystem;

/// <summary>
/// Laptop - specialized Electronics product with additional attributes
/// </summary>
public class Laptop : Electronics
{
    public int RAM { get; set; } // in GB
    public int Storage { get; set; } // in GB

    public Laptop(
        string productId,
        string name,
        decimal price,
        int stockQuantity,
        string description,
        string brand,
        string model,
        int warrantyPeriod,
        double powerUsage,
        DateTime manufacturingDate,
        int ram,
        int storage)
        : base(productId, name, price, stockQuantity, description, brand, model, warrantyPeriod, powerUsage, manufacturingDate)
    {
        RAM = ram;
        Storage = storage;
    }

    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"RAM: {RAM}GB");
        Console.WriteLine($"Storage: {Storage}GB");
    }
}
