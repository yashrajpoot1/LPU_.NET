namespace SmartWarehouseInventory;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║  Smart Warehouse Inventory Prioritization System     ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

        var warehouse = new WarehouseManager();

        try
        {
            // Add Electronics
            warehouse.AddProduct(new Electronics("ELEC001", "Laptop", 1, 50, 10, "Dell", 24));
            warehouse.AddProduct(new Electronics("ELEC002", "Mouse", 5, 200, 50, "Logitech", 12));

            // Add Perishables
            warehouse.AddProduct(new Perishable("PERISH001", "Milk", 2, 100, 20,
                new DateTime(2026, 3, 1), 4.0));
            warehouse.AddProduct(new Perishable("PERISH002", "Bread", 3, 150, 30,
                new DateTime(2026, 2, 20), 22.0));

            // Add Fragile Items
            warehouse.AddProduct(new FragileItem("FRAG001", "Glass Vase", 4, 30, 5,
                "Handle with extreme care", true));
            warehouse.AddProduct(new FragileItem("FRAG002", "Ceramic Plates", 6, 80, 15,
                "Stack carefully", true));

            warehouse.DisplayInventory();

            // Get highest priority products
            Console.WriteLine("\n========== TOP 3 HIGHEST PRIORITY PRODUCTS ==========");
            var topProducts = warehouse.GetHighestPriorityProducts(3);
            foreach (var product in topProducts)
            {
                product.DisplayInfo();
            }

            // Update stock
            Console.WriteLine("\n========== STOCK UPDATES ==========");
            warehouse.UpdateStock("ELEC001", 45);
            warehouse.UpdateStock("PERISH001", 95);

            // Test duplicate SKU
            Console.WriteLine("\n========== TESTING DUPLICATE SKU ==========");
            try
            {
                warehouse.AddProduct(new Electronics("ELEC001", "Duplicate", 1, 10, 5, "Test", 12));
            }
            catch (DuplicateSKUException ex)
            {
                Console.WriteLine($"✗ Exception caught: {ex.Message}");
            }

            // Test low stock exception
            Console.WriteLine("\n========== TESTING LOW STOCK EXCEPTION ==========");
            try
            {
                warehouse.UpdateStock("ELEC001", 5); // Below threshold of 10
            }
            catch (LowStockException ex)
            {
                Console.WriteLine($"✗ Exception caught: {ex.Message}");
                Console.WriteLine($"  Action Required: Reorder {ex.SKU}");
            }

            Console.WriteLine($"\n✅ Total Products in Warehouse: {warehouse.GetTotalProducts()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
