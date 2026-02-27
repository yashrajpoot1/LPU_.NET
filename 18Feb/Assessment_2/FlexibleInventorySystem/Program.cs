using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Exceptions;

namespace FlexibleInventorySystem
{
    /// <summary>
    /// Console application for inventory management
    /// </summary>
    class Program
    {
        private static InventoryManager _inventory = new InventoryManager();

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  FLEXIBLE INVENTORY MANAGEMENT SYSTEM  ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            // Load sample data
            LoadSampleData();

            while (true)
            {
                try
                {
                    DisplayMenu();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddProductMenu();
                            break;
                        case "2":
                            RemoveProductMenu();
                            break;
                        case "3":
                            UpdateQuantityMenu();
                            break;
                        case "4":
                            FindProductMenu();
                            break;
                        case "5":
                            ViewAllProductsMenu();
                            break;
                        case "6":
                            GenerateReportsMenu();
                            break;
                        case "7":
                            CheckLowStockMenu();
                            break;
                        case "8":
                            ViewByCategoryMenu();
                            break;
                        case "9":
                            Console.WriteLine("\nThank you for using the Inventory Management System!");
                            return;
                        default:
                            Console.WriteLine("\n❌ Invalid option. Please try again.");
                            break;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (InventoryException ex)
                {
                    Console.WriteLine($"\n❌ Inventory Error: {ex.Message}");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Unexpected Error: {ex.Message}");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║           MAIN MENU                    ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Find Product");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. Check Low Stock");
            Console.WriteLine("8. View Products by Category");
            Console.WriteLine("9. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddProductMenu()
        {
            Console.WriteLine("\n--- Add New Product ---");
            Console.WriteLine("Select Product Type:");
            Console.WriteLine("1. Electronic Product");
            Console.WriteLine("2. Grocery Product");
            Console.WriteLine("3. Clothing Product");
            Console.Write("\nEnter choice: ");
            string typeChoice = Console.ReadLine();

            try
            {
                switch (typeChoice)
                {
                    case "1":
                        AddElectronicProduct();
                        break;
                    case "2":
                        AddGroceryProduct();
                        break;
                    case "3":
                        AddClothingProduct();
                        break;
                    default:
                        Console.WriteLine("Invalid product type");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
            }
        }

        static void AddElectronicProduct()
        {
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Enter Warranty (months): ");
            int warranty = int.Parse(Console.ReadLine());

            Console.Write("Enter Voltage: ");
            string voltage = Console.ReadLine();

            Console.Write("Is Refurbished? (y/n): ");
            bool isRefurbished = Console.ReadLine().ToLower() == "y";

            var product = new ElectronicProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Electronics",
                Brand = brand,
                WarrantyMonths = warranty,
                Voltage = voltage,
                IsRefurbished = isRefurbished
            };

            if (_inventory.AddProduct(product))
            {
                Console.WriteLine("\n✅ Electronic product added successfully!");
            }
        }

        static void AddGroceryProduct()
        {
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Expiry Date (dd-MM-yyyy): ");
            DateTime expiryDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", null);

            Console.Write("Is Perishable? (y/n): ");
            bool isPerishable = Console.ReadLine().ToLower() == "y";

            Console.Write("Enter Weight (kg): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Enter Storage Temperature: ");
            string storage = Console.ReadLine();

            var product = new GroceryProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Groceries",
                ExpiryDate = expiryDate,
                IsPerishable = isPerishable,
                Weight = weight,
                StorageTemperature = storage
            };

            if (_inventory.AddProduct(product))
            {
                Console.WriteLine("\n✅ Grocery product added successfully!");
            }
        }

        static void AddClothingProduct()
        {
            Console.Write("Enter Product ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Size (XS/S/M/L/XL/XXL): ");
            string size = Console.ReadLine();

            Console.Write("Enter Color: ");
            string color = Console.ReadLine();

            Console.Write("Enter Material: ");
            string material = Console.ReadLine();

            Console.Write("Enter Gender (Men/Women/Unisex): ");
            string gender = Console.ReadLine();

            Console.Write("Enter Season (Summer/Winter/All-season): ");
            string season = Console.ReadLine();

            var product = new ClothingProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Clothing",
                Size = size,
                Color = color,
                Material = material,
                Gender = gender,
                Season = season
            };

            if (_inventory.AddProduct(product))
            {
                Console.WriteLine("\n✅ Clothing product added successfully!");
            }
        }

        static void RemoveProductMenu()
        {
            Console.Write("\nEnter Product ID to remove: ");
            string id = Console.ReadLine();

            if (_inventory.RemoveProduct(id))
            {
                Console.WriteLine("\n✅ Product removed successfully!");
            }
            else
            {
                Console.WriteLine("\n❌ Product not found!");
            }
        }

        static void UpdateQuantityMenu()
        {
            Console.Write("\nEnter Product ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter New Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            if (_inventory.UpdateQuantity(id, quantity))
            {
                Console.WriteLine("\n✅ Quantity updated successfully!");
            }
            else
            {
                Console.WriteLine("\n❌ Product not found!");
            }
        }

        static void FindProductMenu()
        {
            Console.Write("\nEnter Product ID: ");
            string id = Console.ReadLine();

            var product = _inventory.FindProduct(id);
            if (product != null)
            {
                Console.WriteLine("\n--- Product Details ---");
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Category: {product.Category}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Quantity: {product.Quantity}");
                Console.WriteLine($"Value: {product.CalculateValue():C}");
                Console.WriteLine($"Details: {product.GetProductDetails()}");
            }
            else
            {
                Console.WriteLine("\n❌ Product not found!");
            }
        }

        static void ViewAllProductsMenu()
        {
            Console.WriteLine("\n--- All Products ---");
            Console.WriteLine($"{"ID",-10} {"Name",-25} {"Category",-15} {"Price",-12} {"Qty",-8} {"Value",-12}");
            Console.WriteLine(new string('-', 90));

            var products = _inventory.SearchProducts(p => true);
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id,-10} {product.Name,-25} {product.Category,-15} {product.Price,-12:C} {product.Quantity,-8} {product.CalculateValue(),-12:C}");
            }

            Console.WriteLine($"\nTotal Products: {_inventory.GetTotalProductCount()}");
            Console.WriteLine($"Total Inventory Value: {_inventory.GetTotalInventoryValue():C}");
        }

        static void GenerateReportsMenu()
        {
            Console.WriteLine("\n--- Generate Reports ---");
            Console.WriteLine("1. Complete Inventory Report");
            Console.WriteLine("2. Category Summary");
            Console.WriteLine("3. Value Report");
            Console.WriteLine("4. Expiry Report");
            Console.Write("\nEnter choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n" + _inventory.GenerateInventoryReport());
                    break;
                case "2":
                    Console.WriteLine("\n" + _inventory.GenerateCategorySummary());
                    break;
                case "3":
                    Console.WriteLine("\n" + _inventory.GenerateValueReport());
                    break;
                case "4":
                    Console.Write("Enter days threshold: ");
                    int days = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n" + _inventory.GenerateExpiryReport(days));
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        static void CheckLowStockMenu()
        {
            Console.Write("\nEnter threshold quantity: ");
            int threshold = int.Parse(Console.ReadLine());

            var lowStockProducts = _inventory.GetLowStockProducts(threshold);

            if (lowStockProducts.Count == 0)
            {
                Console.WriteLine("\n✅ No low stock products found!");
            }
            else
            {
                Console.WriteLine($"\n--- Low Stock Products (Below {threshold}) ---");
                Console.WriteLine($"{"ID",-10} {"Name",-25} {"Quantity",-10}");
                Console.WriteLine(new string('-', 50));

                foreach (var product in lowStockProducts)
                {
                    Console.WriteLine($"{product.Id,-10} {product.Name,-25} {product.Quantity,-10}");
                }
            }
        }

        static void ViewByCategoryMenu()
        {
            Console.WriteLine("\n--- Available Categories ---");
            var categories = _inventory.GetCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"- {category}");
            }

            Console.Write("\nEnter category name: ");
            string categoryName = Console.ReadLine();

            var products = _inventory.GetProductsByCategory(categoryName);

            if (products.Count == 0)
            {
                Console.WriteLine($"\n❌ No products found in category '{categoryName}'");
            }
            else
            {
                Console.WriteLine($"\n--- Products in {categoryName} ---");
                Console.WriteLine($"{"ID",-10} {"Name",-25} {"Price",-12} {"Qty",-8}");
                Console.WriteLine(new string('-', 60));

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id,-10} {product.Name,-25} {product.Price,-12:C} {product.Quantity,-8}");
                }
            }
        }

        static void LoadSampleData()
        {
            try
            {
                // Electronic Products
                _inventory.AddProduct(new ElectronicProduct
                {
                    Id = "E001",
                    Name = "Dell Laptop",
                    Price = 999.99m,
                    Quantity = 10,
                    Category = "Electronics",
                    Brand = "Dell",
                    WarrantyMonths = 24,
                    Voltage = "110-240V",
                    IsRefurbished = false
                });

                _inventory.AddProduct(new ElectronicProduct
                {
                    Id = "E002",
                    Name = "Samsung Smartphone",
                    Price = 699.99m,
                    Quantity = 25,
                    Category = "Electronics",
                    Brand = "Samsung",
                    WarrantyMonths = 12,
                    Voltage = "5V",
                    IsRefurbished = false
                });

                // Grocery Products
                _inventory.AddProduct(new GroceryProduct
                {
                    Id = "G001",
                    Name = "Fresh Milk",
                    Price = 3.49m,
                    Quantity = 50,
                    Category = "Groceries",
                    ExpiryDate = DateTime.Now.AddDays(7),
                    IsPerishable = true,
                    Weight = 1.0,
                    StorageTemperature = "Refrigerated"
                });

                _inventory.AddProduct(new GroceryProduct
                {
                    Id = "G002",
                    Name = "Bread",
                    Price = 2.99m,
                    Quantity = 30,
                    Category = "Groceries",
                    ExpiryDate = DateTime.Now.AddDays(2),
                    IsPerishable = true,
                    Weight = 0.5,
                    StorageTemperature = "Room temperature"
                });

                // Clothing Products
                _inventory.AddProduct(new ClothingProduct
                {
                    Id = "C001",
                    Name = "Cotton T-Shirt",
                    Price = 19.99m,
                    Quantity = 100,
                    Category = "Clothing",
                    Size = "L",
                    Color = "Blue",
                    Material = "Cotton",
                    Gender = "Unisex",
                    Season = "Summer"
                });

                _inventory.AddProduct(new ClothingProduct
                {
                    Id = "C002",
                    Name = "Winter Jacket",
                    Price = 89.99m,
                    Quantity = 15,
                    Category = "Clothing",
                    Size = "M",
                    Color = "Black",
                    Material = "Polyester",
                    Gender = "Men",
                    Season = "Winter"
                });

                Console.WriteLine("✅ Sample data loaded successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sample data: {ex.Message}");
            }
        }
    }
}
