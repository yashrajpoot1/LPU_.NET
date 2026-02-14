namespace SmartWarehouseInventory;

public class WarehouseManager
{
    private readonly SortedDictionary<int, List<Product>> _inventory = new();
    private readonly Dictionary<string, Product> _skuLookup = new();

    public void AddProduct(Product product)
    {
        if (_skuLookup.ContainsKey(product.SKU))
            throw new DuplicateSKUException(product.SKU);

        if (!_inventory.ContainsKey(product.Priority))
            _inventory[product.Priority] = new List<Product>();

        _inventory[product.Priority].Add(product);
        _skuLookup[product.SKU] = product;

        Console.WriteLine($"✓ Added: {product.Name} (Priority: {product.Priority})");
    }

    public void RemoveProduct(string sku)
    {
        if (!_skuLookup.TryGetValue(sku, out var product))
            throw new InvalidProductException($"Product with SKU {sku} not found");

        _inventory[product.Priority].Remove(product);
        if (_inventory[product.Priority].Count == 0)
            _inventory.Remove(product.Priority);

        _skuLookup.Remove(sku);
        Console.WriteLine($"✓ Removed: {product.Name}");
    }

    public void UpdateStock(string sku, int newStock)
    {
        if (!_skuLookup.TryGetValue(sku, out var product))
            throw new InvalidProductException($"Product with SKU {sku} not found");

        product.Stock = newStock; // Will throw LowStockException if below threshold
        Console.WriteLine($"✓ Updated stock for {product.Name}: {newStock} units");
    }

    public List<Product> GetHighestPriorityProducts(int count = 5)
    {
        var result = new List<Product>();
        foreach (var kvp in _inventory)
        {
            result.AddRange(kvp.Value);
            if (result.Count >= count)
                break;
        }
        return result.Take(count).ToList();
    }

    public void DisplayInventory()
    {
        Console.WriteLine("\n========== WAREHOUSE INVENTORY (Sorted by Priority) ==========");
        foreach (var kvp in _inventory)
        {
            Console.WriteLine($"\n--- Priority Level {kvp.Key} ---");
            foreach (var product in kvp.Value)
            {
                product.DisplayInfo();
            }
        }
    }

    public int GetTotalProducts() => _skuLookup.Count;
}
