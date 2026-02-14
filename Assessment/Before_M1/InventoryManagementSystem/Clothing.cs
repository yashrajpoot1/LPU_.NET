namespace InventoryManagementSystem;

/// <summary>
/// Clothing product with specific attributes
/// </summary>
public class Clothing : ProductBase
{
    public ClothingSize Size { get; set; }
    public string FabricType { get; set; }
    public Gender Gender { get; set; }
    public string Color { get; set; }

    public Clothing(
        string productId,
        string name,
        decimal price,
        int stockQuantity,
        string description,
        ClothingSize size,
        string fabricType,
        Gender gender,
        string color)
        : base(productId, name, price, stockQuantity, description)
    {
        Size = size;
        FabricType = fabricType;
        Gender = gender;
        Color = color;
    }

    public override string GetCategory() => "Clothing";

    public override void DisplayDetails()
    {
        Console.WriteLine($"\n--- {GetCategory()} Product ---");
        Console.WriteLine($"ID: {ProductId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Price: ${Price:F2}");
        Console.WriteLine($"Stock: {StockQuantity} units");
        Console.WriteLine($"Size: {Size}");
        Console.WriteLine($"Fabric: {FabricType}");
        Console.WriteLine($"Gender: {Gender}");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Description: {Description}");
    }
}

public enum ClothingSize
{
    S,
    M,
    L,
    XL,
    XXL
}

public enum Gender
{
    Men,
    Women,
    Unisex
}
