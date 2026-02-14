namespace InventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║   TechNova Retail Solutions - Inventory Management System  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        var inventory = new InventoryManager();

        // Add Electronics Products
        Console.WriteLine("📱 Adding Electronics Products...");
        
        var laptop = new Laptop(
            productId: "ELEC001",
            name: "Dell XPS 15",
            price: 1499.99m,
            stockQuantity: 25,
            description: "High-performance laptop for professionals",
            brand: "Dell",
            model: "XPS 15 9520",
            warrantyPeriod: 24,
            powerUsage: 130,
            manufacturingDate: new DateTime(2024, 1, 15),
            ram: 16,
            storage: 512
        );
        inventory.AddProduct(laptop);

        var mobile = new Electronics(
            productId: "ELEC002",
            name: "iPhone 15 Pro",
            price: 999.99m,
            stockQuantity: 50,
            description: "Latest flagship smartphone",
            brand: "Apple",
            model: "iPhone 15 Pro",
            warrantyPeriod: 12,
            powerUsage: 20,
            manufacturingDate: new DateTime(2024, 9, 1)
        );
        inventory.AddProduct(mobile);

        // Add Grocery Products
        Console.WriteLine("\n🥬 Adding Grocery Products...");
        
        var rice = new Grocery(
            productId: "GROC001",
            name: "Basmati Rice",
            price: 15.99m,
            stockQuantity: 100,
            description: "Premium quality basmati rice",
            expiryDate: new DateTime(2025, 12, 31),
            weight: 5.0,
            isOrganic: true,
            storageTemperature: 25.0
        );
        inventory.AddProduct(rice);

        var milk = new Grocery(
            productId: "GROC002",
            name: "Organic Milk",
            price: 4.99m,
            stockQuantity: 200,
            description: "Fresh organic whole milk",
            expiryDate: new DateTime(2026, 2, 20),
            weight: 1.0,
            isOrganic: true,
            storageTemperature: 4.0
        );
        inventory.AddProduct(milk);

        // Add Clothing Products
        Console.WriteLine("\n👕 Adding Clothing Products...");
        
        var shirt = new Clothing(
            productId: "CLTH001",
            name: "Cotton Casual Shirt",
            price: 29.99m,
            stockQuantity: 75,
            description: "Comfortable cotton shirt for everyday wear",
            size: ClothingSize.L,
            fabricType: "100% Cotton",
            gender: Gender.Men,
            color: "Blue"
        );
        inventory.AddProduct(shirt);

        var jeans = new Clothing(
            productId: "CLTH002",
            name: "Slim Fit Jeans",
            price: 49.99m,
            stockQuantity: 60,
            description: "Modern slim fit denim jeans",
            size: ClothingSize.M,
            fabricType: "Denim",
            gender: Gender.Unisex,
            color: "Dark Blue"
        );
        inventory.AddProduct(jeans);

        // Display all products
        inventory.DisplayAllProducts();

        // Type-safe retrieval examples
        Console.WriteLine("\n\n========== TYPE-SAFE OPERATIONS ==========");
        
        // Get specific laptop (compile-time type safety)
        var retrievedLaptop = inventory.GetProduct<Laptop>("ELEC001");
        if (retrievedLaptop != null)
        {
            Console.WriteLine($"\n✓ Retrieved Laptop: {retrievedLaptop.Name}");
            Console.WriteLine($"  RAM: {retrievedLaptop.RAM}GB, Storage: {retrievedLaptop.Storage}GB");
            // No casting needed - we have full type safety!
        }

        // Display products by category
        inventory.DisplayProductsByCategory<Electronics>();
        inventory.DisplayProductsByCategory<Grocery>();
        inventory.DisplayProductsByCategory<Clothing>();

        // Check for expired groceries
        Console.WriteLine("\n\n========== EXPIRED GROCERIES CHECK ==========");
        var expiredItems = inventory.GetExpiredGroceries();
        Console.WriteLine($"Expired items: {expiredItems.Count}");

        // Calculate total inventory value
        Console.WriteLine("\n========== INVENTORY VALUE ==========");
        Console.WriteLine($"Total Inventory Value: ${inventory.GetTotalInventoryValue():F2}");

        // Demonstrate compile-time type safety
        Console.WriteLine("\n\n========== COMPILE-TIME TYPE SAFETY DEMONSTRATION ==========");
        Console.WriteLine("✓ Electronics products have WarrantyPeriod and PowerUsage");
        Console.WriteLine("✓ Grocery products have ExpiryDate and IsOrganic");
        Console.WriteLine("✓ Clothing products have Size and FabricType");
        Console.WriteLine("✓ No nullable properties or runtime casting needed!");
        Console.WriteLine("✓ Each category has its own strongly-typed class");
        Console.WriteLine("✓ System is extensible - new categories can be added easily");

        Console.WriteLine("\n\n✅ Inventory Management System Demo Complete!");
    }
}
