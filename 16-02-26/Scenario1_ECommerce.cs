using System;
using System.Collections.Generic;
using System.Linq;

namespace Generics_Assessment.Scenario1
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

        /// <summary>
        /// Adds a product with validation
        /// </summary>
        public void AddProduct(T product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null");

            // Rule: Product ID must be unique
            if (_products.Any(p => p.Id == product.Id))
                throw new InvalidOperationException($"Product with ID {product.Id} already exists");

            // Rule: Price must be positive
            if (product.Price <= 0)
                throw new ArgumentException("Price must be positive", nameof(product));

            // Rule: Name cannot be null or empty
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Name cannot be null or empty", nameof(product));

            _products.Add(product);
        }

        /// <summary>
        /// Finds products by predicate
        /// </summary>
        public IEnumerable<T> FindProducts(Func<T, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return _products.Where(predicate);
        }

        /// <summary>
        /// Calculates total inventory value
        /// </summary>
        public decimal CalculateTotalValue()
        {
            return _products.Sum(p => p.Price);
        }

        /// <summary>
        /// Gets all products
        /// </summary>
        public IReadOnlyList<T> GetAllProducts()
        {
            return _products.AsReadOnly();
        }

        /// <summary>
        /// Gets product count
        /// </summary>
        public int Count => _products.Count;
    }

    // 2. Specialized electronic product
    public class ElectronicProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Category Category => Category.Electronics;
        public int WarrantyMonths { get; set; }
        public string Brand { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[Electronics] {Name} by {Brand} - ${Price:F2} (Warranty: {WarrantyMonths} months)";
        }
    }

    // Clothing product
    public class ClothingProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Category Category => Category.Clothing;
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[Clothing] {Name} - Size: {Size}, Color: {Color} - ${Price:F2}";
        }
    }

    // Book product
    public class BookProduct : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Category Category => Category.Books;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"[Book] {Name} by {Author} - ${Price:F2}";
        }
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
                throw new ArgumentException("Discount must be between 0 and 100", nameof(discountPercentage));

            _product = product;
            _discountPercentage = discountPercentage;
        }

        public T Product => _product;
        public decimal DiscountPercentage => _discountPercentage;

        /// <summary>
        /// Calculates price with discount
        /// </summary>
        public decimal DiscountedPrice => _product.Price * (1 - _discountPercentage / 100);

        /// <summary>
        /// Gets the discount amount
        /// </summary>
        public decimal DiscountAmount => _product.Price - DiscountedPrice;

        public override string ToString()
        {
            return $"{_product.Name} - Original: ${_product.Price:F2}, " +
                   $"Discount: {_discountPercentage}%, " +
                   $"Final: ${DiscountedPrice:F2} (Save ${DiscountAmount:F2})";
        }
    }

    // 4. Inventory manager with constraints
    public class InventoryManager
    {
        /// <summary>
        /// Processes products with various operations
        /// </summary>
        public void ProcessProducts<T>(IEnumerable<T> products) where T : IProduct
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            var productList = products.ToList();

            // a) Print all product names and prices
            Console.WriteLine("\n=== All Products ===");
            foreach (var product in productList)
            {
                Console.WriteLine($"{product.Name}: ${product.Price:F2}");
            }

            // b) Find the most expensive product
            var mostExpensive = productList.OrderByDescending(p => p.Price).FirstOrDefault();
            if (mostExpensive != null)
            {
                Console.WriteLine($"\nMost Expensive: {mostExpensive.Name} - ${mostExpensive.Price:F2}");
            }

            // c) Group products by category
            Console.WriteLine("\n=== Products by Category ===");
            var grouped = productList.GroupBy(p => p.Category);
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var product in group)
                {
                    Console.WriteLine($"  - {product.Name}: ${product.Price:F2}");
                }
            }

            // d) Apply 10% discount to Electronics over $500
            Console.WriteLine("\n=== Electronics Over $500 (10% Discount) ===");
            var expensiveElectronics = productList
                .Where(p => p.Category == Category.Electronics && p.Price > 500);

            foreach (var product in expensiveElectronics)
            {
                var discounted = new DiscountedProduct<T>(product, 10);
                Console.WriteLine(discounted);
            }
        }

        /// <summary>
        /// Updates prices using a delegate function
        /// </summary>
        public void UpdatePrices<T>(List<T> products, Func<T, decimal> priceAdjuster)
            where T : IProduct
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            if (priceAdjuster == null)
                throw new ArgumentNullException(nameof(priceAdjuster));

            Console.WriteLine("\n=== Updating Prices ===");
            foreach (var product in products)
            {
                try
                {
                    decimal oldPrice = product.Price;
                    decimal newPrice = priceAdjuster(product);

                    if (newPrice <= 0)
                    {
                        Console.WriteLine($"Warning: Invalid price for {product.Name}, skipping");
                        continue;
                    }

                    // Note: Since IProduct.Price is read-only, we'd need to use reflection
                    // or have a mutable implementation. For demonstration:
                    Console.WriteLine($"{product.Name}: ${oldPrice:F2} → ${newPrice:F2}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating {product.Name}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Applies bulk discount to products
        /// </summary>
        public List<DiscountedProduct<T>> ApplyBulkDiscount<T>(
            IEnumerable<T> products,
            Func<T, bool> condition,
            decimal discountPercentage) where T : IProduct
        {
            var discountedProducts = new List<DiscountedProduct<T>>();

            foreach (var product in products.Where(condition))
            {
                try
                {
                    discountedProducts.Add(new DiscountedProduct<T>(product, discountPercentage));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error applying discount to {product.Name}: {ex.Message}");
                }
            }

            return discountedProducts;
        }
    }

    // Demo class for Scenario 1
    public static class Scenario1Demo
    {
        public static void Run()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║   SCENARIO 1: E-Commerce Inventory System                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

            try
            {
                // Create repository
                var repository = new ProductRepository<IProduct>();

                // a) Create sample inventory with at least 5 products
                Console.WriteLine("\n--- Adding Products to Inventory ---");

                var products = new List<IProduct>
                {
                    new ElectronicProduct
                    {
                        Id = 1,
                        Name = "iPhone 15 Pro",
                        Price = 999.99m,
                        Brand = "Apple",
                        WarrantyMonths = 12
                    },
                    new ElectronicProduct
                    {
                        Id = 2,
                        Name = "Samsung 4K TV",
                        Price = 799.99m,
                        Brand = "Samsung",
                        WarrantyMonths = 24
                    },
                    new ClothingProduct
                    {
                        Id = 3,
                        Name = "Levi's Jeans",
                        Price = 79.99m,
                        Size = "32",
                        Color = "Blue"
                    },
                    new BookProduct
                    {
                        Id = 4,
                        Name = "Clean Code",
                        Price = 45.99m,
                        Author = "Robert C. Martin",
                        ISBN = "978-0132350884"
                    },
                    new ElectronicProduct
                    {
                        Id = 5,
                        Name = "MacBook Pro",
                        Price = 2499.99m,
                        Brand = "Apple",
                        WarrantyMonths = 12
                    }
                };

                foreach (var product in products)
                {
                    repository.AddProduct(product);
                    Console.WriteLine($"✓ Added: {product}");
                }

                // b) Calculate total value before discount
                Console.WriteLine($"\n--- Total Inventory Value ---");
                Console.WriteLine($"Total Value: ${repository.CalculateTotalValue():F2}");
                Console.WriteLine($"Total Products: {repository.Count}");

                // c) Finding products by brand (for electronics)
                Console.WriteLine("\n--- Finding Apple Products ---");
                var appleProducts = repository.FindProducts(p =>
                    p is ElectronicProduct ep && ep.Brand == "Apple");

                foreach (var product in appleProducts)
                {
                    Console.WriteLine($"  {product}");
                }

                // d) Applying discounts
                Console.WriteLine("\n--- Applying Discounts ---");
                var manager = new InventoryManager();

                // 20% discount on all clothing
                var clothingDiscounts = manager.ApplyBulkDiscount(
                    repository.GetAllProducts(),
                    p => p.Category == Category.Clothing,
                    20);

                Console.WriteLine("\nClothing with 20% discount:");
                foreach (var discounted in clothingDiscounts)
                {
                    Console.WriteLine($"  {discounted}");
                }

                // 15% discount on electronics over $1000
                var premiumElectronicsDiscounts = manager.ApplyBulkDiscount(
                    repository.GetAllProducts(),
                    p => p.Category == Category.Electronics && p.Price > 1000,
                    15);

                Console.WriteLine("\nPremium Electronics with 15% discount:");
                foreach (var discounted in premiumElectronicsDiscounts)
                {
                    Console.WriteLine($"  {discounted}");
                }

                // e) Calculate total value after discount
                decimal totalAfterDiscount = repository.GetAllProducts()
                    .Where(p => p.Category == Category.Clothing)
                    .Sum(p => p.Price * 0.8m) +
                    repository.GetAllProducts()
                    .Where(p => p.Category == Category.Electronics && p.Price > 1000)
                    .Sum(p => p.Price * 0.85m) +
                    repository.GetAllProducts()
                    .Where(p => p.Category != Category.Clothing &&
                               !(p.Category == Category.Electronics && p.Price > 1000))
                    .Sum(p => p.Price);

                Console.WriteLine($"\n--- Value Comparison ---");
                Console.WriteLine($"Original Total: ${repository.CalculateTotalValue():F2}");
                Console.WriteLine($"After Discounts: ${totalAfterDiscount:F2}");
                Console.WriteLine($"Total Savings: ${repository.CalculateTotalValue() - totalAfterDiscount:F2}");

                // f) Process products with manager
                manager.ProcessProducts(repository.GetAllProducts());

                // g) Demonstrate validation errors
                Console.WriteLine("\n--- Testing Validation ---");
                try
                {
                    repository.AddProduct(new BookProduct
                    {
                        Id = 1, // Duplicate ID
                        Name = "Test Book",
                        Price = 20
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Validation Error: {ex.Message}");
                }

                try
                {
                    repository.AddProduct(new BookProduct
                    {
                        Id = 10,
                        Name = "Invalid Book",
                        Price = -10 // Invalid price
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Validation Error: {ex.Message}");
                }

                Console.WriteLine("\n✓ Scenario 1 completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error in Scenario 1: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
