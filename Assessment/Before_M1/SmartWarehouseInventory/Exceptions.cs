namespace SmartWarehouseInventory;

// Exception Hierarchy
public class InventoryException : Exception
{
    public InventoryException(string message) : base(message) { }
}

public class LowStockException : InventoryException
{
    public string SKU { get; }
    public int CurrentStock { get; }
    public int Threshold { get; }

    public LowStockException(string sku, int currentStock, int threshold)
        : base($"Low stock alert for SKU {sku}: Current={currentStock}, Threshold={threshold}")
    {
        SKU = sku;
        CurrentStock = currentStock;
        Threshold = threshold;
    }
}

public class DuplicateSKUException : InventoryException
{
    public string SKU { get; }

    public DuplicateSKUException(string sku)
        : base($"Product with SKU {sku} already exists in inventory")
    {
        SKU = sku;
    }
}

public class InvalidProductException : InventoryException
{
    public InvalidProductException(string message) : base(message) { }
}
