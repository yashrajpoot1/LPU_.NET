namespace SmartWarehouseInventory;

public abstract class Product
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; } // 1-10
    private int _stock;
    public int StockThreshold { get; set; }

    public int Stock
    {
        get => _stock;
        set
        {
            if (value < 0)
                throw new InvalidProductException("Stock cannot be negative");
            
            _stock = value;
            ValidateStock();
        }
    }

    protected Product(string sku, string name, int priority, int stock, int stockThreshold)
    {
        if (priority < 1 || priority > 10)
            throw new InvalidProductException("Priority must be between 1 and 10");

        SKU = sku;
        Name = name;
        Priority = priority;
        StockThreshold = stockThreshold;
        Stock = stock;
    }

    protected void ValidateStock()
    {
        if (_stock < StockThreshold)
            throw new LowStockException(SKU, _stock, StockThreshold);
    }

    public abstract string GetProductType();
    public abstract void DisplayInfo();
}

public class Electronics : Product
{
    public string Brand { get; set; }
    public int WarrantyMonths { get; set; }

    public Electronics(string sku, string name, int priority, int stock, int stockThreshold,
                      string brand, int warrantyMonths)
        : base(sku, name, priority, stock, stockThreshold)
    {
        Brand = brand;
        WarrantyMonths = warrantyMonths;
    }

    public override string GetProductType() => "Electronics";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetProductType()}] {Name} (SKU: {SKU})");
        Console.WriteLine($"  Brand: {Brand}, Warranty: {WarrantyMonths} months");
        Console.WriteLine($"  Priority: {Priority}, Stock: {Stock}, Threshold: {StockThreshold}");
    }
}

public class Perishable : Product
{
    public DateTime ExpiryDate { get; set; }
    public double Temperature { get; set; }

    public Perishable(string sku, string name, int priority, int stock, int stockThreshold,
                     DateTime expiryDate, double temperature)
        : base(sku, name, priority, stock, stockThreshold)
    {
        ExpiryDate = expiryDate;
        Temperature = temperature;
    }

    public override string GetProductType() => "Perishable";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetProductType()}] {Name} (SKU: {SKU})");
        Console.WriteLine($"  Expiry: {ExpiryDate:yyyy-MM-dd}, Temp: {Temperature}°C");
        Console.WriteLine($"  Priority: {Priority}, Stock: {Stock}, Threshold: {StockThreshold}");
    }
}

public class FragileItem : Product
{
    public string HandlingInstructions { get; set; }
    public bool RequiresSpecialPackaging { get; set; }

    public FragileItem(string sku, string name, int priority, int stock, int stockThreshold,
                      string handlingInstructions, bool requiresSpecialPackaging)
        : base(sku, name, priority, stock, stockThreshold)
    {
        HandlingInstructions = handlingInstructions;
        RequiresSpecialPackaging = requiresSpecialPackaging;
    }

    public override string GetProductType() => "Fragile Item";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[{GetProductType()}] {Name} (SKU: {SKU})");
        Console.WriteLine($"  Handling: {HandlingInstructions}");
        Console.WriteLine($"  Special Packaging: {(RequiresSpecialPackaging ? "Yes" : "No")}");
        Console.WriteLine($"  Priority: {Priority}, Stock: {Stock}, Threshold: {StockThreshold}");
    }
}
