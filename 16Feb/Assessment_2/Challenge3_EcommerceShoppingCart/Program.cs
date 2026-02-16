using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge3_EcommerceShoppingCart
{
    // Base product class
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"{Name} (${Price:F2})";
        }
    }

    // Specialized product types
    public class Electronics : Product
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }
    }

    public class Clothing : Product
    {
        public string Size { get; set; }
        public string Color { get; set; }
    }

    public class Books : Product
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
    }

    // Generic shopping cart
    public class ShoppingCart<T> where T : Product
    {
        private Dictionary<T, int> _cartItems = new Dictionary<T, int>();

        // Add product to cart
        public void AddToCart(T product, int quantity)
        {
            if (product == null)
            {
                Console.WriteLine("Error: Cannot add null product.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Error: Quantity must be positive.");
                return;
            }

            if (_cartItems.ContainsKey(product))
            {
                _cartItems[product] += quantity;
                Console.WriteLine($"Updated: {product.Name} - New quantity: {_cartItems[product]}");
            }
            else
            {
                _cartItems[product] = quantity;
                Console.WriteLine($"Added: {product.Name} x{quantity}");
            }
        }

        // Remove product from cart
        public bool RemoveFromCart(T product, int quantity = int.MaxValue)
        {
            if (!_cartItems.ContainsKey(product))
                return false;

            if (quantity >= _cartItems[product])
            {
                _cartItems.Remove(product);
            }
            else
            {
                _cartItems[product] -= quantity;
            }

            return true;
        }

        // Calculate total with discount delegate
        public double CalculateTotal(Func<T, double, double> discountCalculator = null)
        {
            double total = 0;
            foreach (var item in _cartItems)
            {
                double price = item.Key.Price * item.Value;
                
                if (discountCalculator != null)
                {
                    price = discountCalculator(item.Key, price);
                }
                
                total += price;
            }
            return total;
        }

        // Get top N expensive items using LINQ
        public List<T> GetTopExpensiveItems(int n)
        {
            return _cartItems.Keys
                .OrderByDescending(p => p.Price)
                .Take(n)
                .ToList();
        }

        // Additional methods
        public int GetItemCount()
        {
            return _cartItems.Sum(item => item.Value);
        }

        public IEnumerable<KeyValuePair<T, int>> GetCartItems()
        {
            return _cartItems;
        }

        public void Clear()
        {
            _cartItems.Clear();
            Console.WriteLine("Cart cleared.");
        }

        public double GetSubtotal()
        {
            return _cartItems.Sum(item => item.Key.Price * item.Value);
        }

        public IEnumerable<T> GetProductsAbovePrice(double minPrice)
        {
            return _cartItems.Keys.Where(p => p.Price >= minPrice);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== E-commerce Shopping Cart System ===\n");

            // Test Case 1: Electronics cart
            Console.WriteLine("--- Electronics Shopping Cart ---");
            ShoppingCart<Electronics> electronicsCart = new ShoppingCart<Electronics>();

            var laptop = new Electronics { Id = 1, Name = "Laptop", Price = 999.99, Brand = "Dell", WarrantyMonths = 24 };
            var mouse = new Electronics { Id = 2, Name = "Mouse", Price = 29.99, Brand = "Logitech", WarrantyMonths = 12 };
            var keyboard = new Electronics { Id = 3, Name = "Keyboard", Price = 79.99, Brand = "Corsair", WarrantyMonths = 12 };

            electronicsCart.AddToCart(laptop, 1);
            electronicsCart.AddToCart(mouse, 2);
            electronicsCart.AddToCart(keyboard, 1);

            // Test Case 2: Calculate total without discount
            Console.WriteLine($"\nSubtotal: ${electronicsCart.GetSubtotal():F2}");
            Console.WriteLine($"Total items: {electronicsCart.GetItemCount()}");

            // Test Case 3: Apply 10% discount for items over $100
            Console.WriteLine("\n--- Applying Discount (10% off items > $100) ---");
            double total = electronicsCart.CalculateTotal((product, price) =>
                price > 100 ? price * 0.9 : price);
            Console.WriteLine($"Total with discount: ${total:F2}");

            // Test Case 4: Get top expensive items
            Console.WriteLine("\n--- Top Expensive Items ---");
            var topItems = electronicsCart.GetTopExpensiveItems(2);
            foreach (var item in topItems)
            {
                Console.WriteLine($"  - {item}");
            }
            Console.WriteLine($"Most expensive: {topItems[0].Name}"); // Should output: Laptop

            // Test Case 5: Clothing cart with different discount strategy
            Console.WriteLine("\n--- Clothing Shopping Cart ---");
            ShoppingCart<Clothing> clothingCart = new ShoppingCart<Clothing>();

            var shirt = new Clothing { Id = 1, Name = "T-Shirt", Price = 25.99, Size = "M", Color = "Blue" };
            var jeans = new Clothing { Id = 2, Name = "Jeans", Price = 59.99, Size = "32", Color = "Black" };
            var jacket = new Clothing { Id = 3, Name = "Jacket", Price = 129.99, Size = "L", Color = "Gray" };

            clothingCart.AddToCart(shirt, 3);
            clothingCart.AddToCart(jeans, 2);
            clothingCart.AddToCart(jacket, 1);

            // Buy 2 get 20% off discount
            Console.WriteLine("\n--- Applying Bulk Discount (20% off for qty >= 2) ---");
            double clothingTotal = clothingCart.CalculateTotal((product, price) =>
            {
                int qty = clothingCart.GetCartItems().First(item => item.Key == product).Value;
                return qty >= 2 ? price * 0.8 : price;
            });
            Console.WriteLine($"Total with bulk discount: ${clothingTotal:F2}");

            // Test Case 6: Books cart with tiered discount
            Console.WriteLine("\n--- Books Shopping Cart ---");
            ShoppingCart<Books> booksCart = new ShoppingCart<Books>();

            var book1 = new Books { Id = 1, Name = "Clean Code", Price = 45.99, Author = "Robert Martin", ISBN = "978-0132350884" };
            var book2 = new Books { Id = 2, Name = "Design Patterns", Price = 54.99, Author = "Gang of Four", ISBN = "978-0201633610" };
            var book3 = new Books { Id = 3, Name = "Refactoring", Price = 49.99, Author = "Martin Fowler", ISBN = "978-0134757599" };

            booksCart.AddToCart(book1, 1);
            booksCart.AddToCart(book2, 1);
            booksCart.AddToCart(book3, 1);

            // Tiered discount: $40-50 = 10% off, $50+ = 15% off
            Console.WriteLine("\n--- Applying Tiered Discount ---");
            double booksTotal = booksCart.CalculateTotal((product, price) =>
            {
                if (product.Price >= 50)
                    return price * 0.85; // 15% off
                else if (product.Price >= 40)
                    return price * 0.90; // 10% off
                return price;
            });
            Console.WriteLine($"Total with tiered discount: ${booksTotal:F2}");

            // Test Case 7: Complex LINQ queries
            Console.WriteLine("\n--- Products Above $50 in Electronics Cart ---");
            var expensiveElectronics = electronicsCart.GetProductsAbovePrice(50);
            foreach (var product in expensiveElectronics)
            {
                Console.WriteLine($"  - {product}");
            }

            // Test Case 8: Update quantity
            Console.WriteLine("\n--- Updating Quantities ---");
            electronicsCart.AddToCart(mouse, 1); // Add one more mouse
            Console.WriteLine($"New total items: {electronicsCart.GetItemCount()}");

            // Test Case 9: Cart summary
            Console.WriteLine("\n--- Electronics Cart Summary ---");
            foreach (var item in electronicsCart.GetCartItems())
            {
                Console.WriteLine($"  {item.Key.Name} x{item.Value} = ${item.Key.Price * item.Value:F2}");
            }

            Console.WriteLine("\n=== Demo Completed Successfully ===");
        }
    }
}
