using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenario1_ECommerce
{
    // Base product interface
    public interface IProduct
    {
        int Id { get; }
        string Name { get; }
        decimal Price { get; }
        Category Category { get; }
    }

    public enum Category { Electronics, Clothing, Books, Groceries }

    // 1. Generic repository for products
    public class ProductRepository<T> where T : class, IProduct
    {
        private List<T> _products = new List<T>();

        public void AddProduct(T product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be null or empty");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be positive");

            if (_products.Any(p => p.Id == product.Id))
                throw new InvalidOperationException($"Product with ID {product.Id} already exists");

            _products.Add(product);
        }

        public IEnumerable<T> FindProducts(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _products.Where(predicate);
        }

        public decimal CalculateTotalValue()
        {
            return _products.Sum(p => p.Price);
        }

        public IEnumerable<T> GetAllProducts() => _products;
    }

    // 2. Specialized electronic product
    public class ElectronicProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category => Category.Electronics;
        public int WarrantyMonths { get; set; }
        public string Brand { get; set; }
    }

    // Clothing product
    public class ClothingProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category => Category.Clothing;
        public string Size { get; set; }
        public string Color { get; set; }
    }

    // Book product
    public class BookProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category => Category.Books;
        public string Author { get; set; }
        public string ISBN { get; set; }
    }

    // 3. Discounted product wrapper
    public class DiscountedProduct<T> where T : IProduct
    {
        private T _product;
        private decimal _discountPercentage;

        public DiscountedProduct(T product, decimal discountPercentage)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount percentage must be between 0 and 100");

            _product = product;
            _discountPercentage = discountPercentage;
        }

        public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);

        public T Product => _product;

        public override string ToString()
        {
            return $"{_product.Name} - Original: ${_product.Price:F2}, " +
                   $"Discount: {_discountPercentage}%, Final: ${DiscountedPrice:F2}";
        }
    }

    // 4. Inventory manager with constraints
    public class InventoryManager
    {
        public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
        {
            if (products == null || !products.Any())
            {
                Console.WriteLine("No products to process");
                return;
            }

            // a) Print all product names and prices
            Console.WriteLine("\n=== All Products ===");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}: ${product.Price:F2}");
            }

            // b) Find the most expensive product
            var mostExpensive = products.OrderByDescending(p => p.Price).FirstOrDefault();
            Console.WriteLine($"\nMost Expensive: {mostExpensive?.Name} - ${mostExpensive?.Price:F2}");

            // c) Group products by category
            Console.WriteLine("\n=== Products by Category ===");
            var grouped = products.GroupBy(p => p.Category);
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var product in group)
                {
                    Console.WriteLine($"  - {product.Name}: ${product.Price:F2}");
                }
            }

            // d) Apply 10% discount to Electronics over $500
            Console.WriteLine("\n=== Electronics Over $500 with 10% Discount ===");
            var expensiveElectronics = products
                .Where(p => p.Category == Category.Electronics && p.Price > 500);

            foreach (var product in expensiveElectronics)
            {
                var discounted = new DiscountedProduct<T>(product, 10);
                Console.WriteLine(discounted.ToString());
            }
        }

        public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
            where T : IProduct
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            if (priceAdjuster == null)
                throw new ArgumentNullException(nameof(priceAdjuster));

            foreach (var product in products)
            {
                try
                {
                    decimal newPrice = priceAdjuster(product);
                    if (newPrice <= 0)
                    {
                        Console.WriteLine($"Warning: Invalid price for {product.Name}, skipping");
                        continue;
                    }

                    Console.WriteLine($"Updated {product.Name}: ${product.Price:F2} -> ${newPrice:F2}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating price for {product.Name}: {ex.Message}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== E-Commerce Inventory System Demo ===\n");

            try
            {
                // Create repository
                var repository = new ProductRepository<IProduct>();

                // Create sample products
                var laptop = new ElectronicProduct
                {
                    Id = 1,
                    Name = "Dell XPS 15",
                    Price = 1299.99m,
                    Brand = "Dell",
                    WarrantyMonths = 24
                };

                var phone = new ElectronicProduct
                {
                    Id = 2,
                    Name = "iPhone 15",
                    Price = 999.99m,
                    Brand = "Apple",
                    WarrantyMonths = 12
                };

                var shirt = new ClothingProduct
                {
                    Id = 3,
                    Name = "Cotton T-Shirt",
                    Price = 29.99m,
                    Size = "M",
                    Color = "Blue"
                };

                var book = new BookProduct
                {
                    Id = 4,
                    Name = "Clean Code",
                    Price = 45.99m,
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884"
                };

                var headphones = new ElectronicProduct
                {
                    Id = 5,
                    Name = "Sony WH-1000XM5",
                    Price = 399.99m,
                    Brand = "Sony",
                    WarrantyMonths = 12
                };

                // Add products with validation
                Console.WriteLine("=== Adding Products ===");
                repository.AddProduct(laptop);
                repository.AddProduct(phone);
                repository.AddProduct(shirt);
                repository.AddProduct(book);
                repository.AddProduct(headphones);
                Console.WriteLine("All products added successfully!\n");

                // Calculate total value before discount
                Console.WriteLine($"Total Inventory Value: ${repository.CalculateTotalValue():F2}\n");

                // Find products by brand (for electronics)
                Console.WriteLine("=== Finding Dell Products ===");
                var dellProducts = repository.FindProducts(p =>
                    p is ElectronicProduct ep && ep.Brand == "Dell");
                foreach (var product in dellProducts)
                {
                    Console.WriteLine($"Found: {product.Name} - ${product.Price:F2}");
                }

                // Apply discounts
                Console.WriteLine("\n=== Applying Discounts ===");
                var discountedLaptop = new DiscountedProduct<IProduct>(laptop, 15);
                Console.WriteLine(discountedLaptop.ToString());

                var discountedPhone = new DiscountedProduct<IProduct>(phone, 10);
                Console.WriteLine(discountedPhone.ToString());

                // Process all products
                var manager = new InventoryManager();
                manager.ProcessProducts(repository.GetAllProducts());

                // Test validation - duplicate ID
                Console.WriteLine("\n=== Testing Validation ===");
                try
                {
                    var duplicate = new ElectronicProduct
                    {
                        Id = 1,
                        Name = "Duplicate",
                        Price = 100,
                        Brand = "Test"
                    };
                    repository.AddProduct(duplicate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Expected error: {ex.Message}");
                }

                // Test validation - invalid price
                try
                {
                    var invalidPrice = new ElectronicProduct
                    {
                        Id = 10,
                        Name = "Invalid",
                        Price = -50,
                        Brand = "Test"
                    };
                    repository.AddProduct(invalidPrice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Expected error: {ex.Message}");
                }

                Console.WriteLine("\n=== Demo Completed Successfully ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
