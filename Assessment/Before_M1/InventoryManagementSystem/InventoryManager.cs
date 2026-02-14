namespace InventoryManagementSystem;

/// <summary>
/// Generic inventory manager that handles all product types in a type-safe manner
/// </summary>
public class InventoryManager
{
    private readonly List<ProductBase> _products = new();

    public void AddProduct(ProductBase product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (_products.Any(p => p.ProductId == product.ProductId))
            throw new InvalidOperationException($"Product with ID {product.ProductId} already exists.");

        _products.Add(product);
        Console.WriteLine($"✓ Added {product.GetCategory()} product: {product.Name}");
    }

    public T? GetProduct<T>(string productId) where T : ProductBase
    {
        return _products.OfType<T>().FirstOrDefault(p => p.ProductId == productId);
    }

    public List<T> GetProductsByCategory<T>() where T : ProductBase
    {
        return _products.OfType<T>().ToList();
    }

    public void DisplayAllProducts()
    {
        Console.WriteLine("\n========== INVENTORY SUMMARY ==========");
        Console.WriteLine($"Total Products: {_products.Count}");
        
        foreach (var product in _products)
        {
            product.DisplayDetails();
        }
    }

    public void DisplayProductsByCategory<T>() where T : ProductBase
    {
        var products = GetProductsByCategory<T>();
        Console.WriteLine($"\n========== {typeof(T).Name.ToUpper()} PRODUCTS ==========");
        Console.WriteLine($"Count: {products.Count}");
        
        foreach (var product in products)
        {
            product.DisplayDetails();
        }
    }

    public void UpdateStock(string productId, int quantity)
    {
        var product = _products.FirstOrDefault(p => p.ProductId == productId);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {productId} not found.");

        product.StockQuantity = quantity;
        Console.WriteLine($"✓ Updated stock for {product.Name}: {quantity} units");
    }

    public List<Grocery> GetExpiredGroceries()
    {
        return _products.OfType<Grocery>().Where(g => g.IsExpired()).ToList();
    }

    public decimal GetTotalInventoryValue()
    {
        return _products.Sum(p => p.Price * p.StockQuantity);
    }
}
