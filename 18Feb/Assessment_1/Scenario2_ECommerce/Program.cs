using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceOrderManagement
{
    // Custom Exceptions
    public class OutOfStockException : Exception
    {
        public OutOfStockException(string message) : base(message) { }
    }

    public class OrderAlreadyShippedException : Exception
    {
        public OrderAlreadyShippedException(string message) : base(message) { }
    }

    public class CustomerBlacklistedException : Exception
    {
        public CustomerBlacklistedException(string message) : base(message) { }
    }

    // Discount Strategy Interface
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
        string GetDiscountName();
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        private decimal _percentage;
        public PercentageDiscount(decimal percentage) => _percentage = percentage;
        public decimal ApplyDiscount(decimal amount) => amount * (1 - _percentage / 100);
        public string GetDiscountName() => $"{_percentage}% Discount";
    }

    public class FlatDiscount : IDiscountStrategy
    {
        private decimal _amount;
        public FlatDiscount(decimal amount) => _amount = amount;
        public decimal ApplyDiscount(decimal amount) => Math.Max(0, amount - _amount);
        public string GetDiscountName() => $"Flat ₹{_amount} Off";
    }

    public class FestivalDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount) => amount * 0.75m; // 25% off
        public string GetDiscountName() => "Festival Special - 25% Off";
    }

    // Entities
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsBlacklisted { get; set; }

        public Customer(int id, string name, string email, bool isBlacklisted = false)
        {
            Id = id;
            Name = name;
            Email = email;
            IsBlacklisted = isBlacklisted;
        }
    }

    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal TotalPrice() => Product.Price * Quantity;
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public IDiscountStrategy DiscountStrategy { get; set; }

        public Order(int orderId, Customer customer)
        {
            OrderId = orderId;
            Customer = customer;
            OrderItems = new List<OrderItem>();
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }

        public decimal GetTotalAmount()
        {
            decimal total = OrderItems.Sum(item => item.TotalPrice());
            return DiscountStrategy != null ? DiscountStrategy.ApplyDiscount(total) : total;
        }

        public void CancelOrder()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                throw new OrderAlreadyShippedException("Cannot cancel shipped or delivered order");

            Status = OrderStatus.Cancelled;
            
            // Restore stock
            foreach (var item in OrderItems)
            {
                item.Product.Stock += item.Quantity;
            }
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();
        static List<Customer> customers = new List<Customer>();
        static List<Order> orders = new List<Order>();
        static Dictionary<int, Product> productDict = new Dictionary<int, Product>();
        static int orderIdCounter = 1;

        static void Main(string[] args)
        {
            InitializeSampleData();

            while (true)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║   E-COMMERCE ORDER MANAGEMENT          ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. View Customers");
                Console.WriteLine("3. Place Order");
                Console.WriteLine("4. View Orders");
                Console.WriteLine("5. Update Order Status");
                Console.WriteLine("6. Cancel Order");
                Console.WriteLine("7. LINQ Reports");
                Console.WriteLine("8. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": ViewProducts(); break;
                        case "2": ViewCustomers(); break;
                        case "3": PlaceOrder(); break;
                        case "4": ViewOrders(); break;
                        case "5": UpdateOrderStatus(); break;
                        case "6": CancelOrder(); break;
                        case "7": ShowLinqReports(); break;
                        case "8": return;
                        default: Console.WriteLine("Invalid choice!"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
            }
        }

        static void InitializeSampleData()
        {
            // Products
            products.Add(new Product(1, "Laptop", 45000, 15));
            products.Add(new Product(2, "Smartphone", 25000, 8));
            products.Add(new Product(3, "Headphones", 2000, 50));
            products.Add(new Product(4, "Keyboard", 1500, 30));
            products.Add(new Product(5, "Mouse", 800, 5));

            foreach (var product in products)
                productDict[product.Id] = product;

            // Customers
            customers.Add(new Customer(1, "Amit Sharma", "amit@email.com"));
            customers.Add(new Customer(2, "Priya Patel", "priya@email.com"));
            customers.Add(new Customer(3, "Rahul Kumar", "rahul@email.com"));
            customers.Add(new Customer(4, "Sneha Singh", "sneha@email.com", true)); // Blacklisted

            // Sample Orders
            var order1 = new Order(orderIdCounter++, customers[0]);
            order1.OrderItems.Add(new OrderItem(products[0], 1));
            order1.OrderItems.Add(new OrderItem(products[2], 2));
            order1.Status = OrderStatus.Delivered;
            order1.OrderDate = DateTime.Now.AddDays(-10);
            orders.Add(order1);

            var order2 = new Order(orderIdCounter++, customers[1]);
            order2.OrderItems.Add(new OrderItem(products[1], 2));
            order2.Status = OrderStatus.Shipped;
            order2.OrderDate = DateTime.Now.AddDays(-3);
            orders.Add(order2);
        }

        static void ViewProducts()
        {
            Console.WriteLine("\n--- Available Products ---");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Price",-12} {"Stock",-10}");
            Console.WriteLine(new string('-', 50));
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id,-5} {product.Name,-20} ₹{product.Price,-10} {product.Stock,-10}");
            }
        }

        static void ViewCustomers()
        {
            Console.WriteLine("\n--- Customers ---");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Email",-25} {"Status",-15}");
            Console.WriteLine(new string('-', 70));
            foreach (var customer in customers)
            {
                string status = customer.IsBlacklisted ? "BLACKLISTED" : "Active";
                Console.WriteLine($"{customer.Id,-5} {customer.Name,-20} {customer.Email,-25} {status,-15}");
            }
        }

        static void PlaceOrder()
        {
            Console.Write("\nEnter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                Console.WriteLine("❌ Customer not found!");
                return;
            }

            if (customer.IsBlacklisted)
                throw new CustomerBlacklistedException($"Customer {customer.Name} is blacklisted and cannot place orders");

            var order = new Order(orderIdCounter++, customer);

            while (true)
            {
                Console.Write("\nEnter Product ID (0 to finish): ");
                int productId = int.Parse(Console.ReadLine());

                if (productId == 0) break;

                if (!productDict.ContainsKey(productId))
                {
                    Console.WriteLine("❌ Product not found!");
                    continue;
                }

                var product = productDict[productId];

                Console.Write("Enter Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                if (product.Stock < quantity)
                    throw new OutOfStockException($"Insufficient stock for {product.Name}. Available: {product.Stock}");

                product.Stock -= quantity;
                order.OrderItems.Add(new OrderItem(product, quantity));
                Console.WriteLine($"✅ Added {quantity} x {product.Name}");
            }

            if (order.OrderItems.Count == 0)
            {
                Console.WriteLine("❌ No items added to order!");
                return;
            }

            // Apply discount
            Console.WriteLine("\nSelect Discount:");
            Console.WriteLine("1. No Discount");
            Console.WriteLine("2. 10% Percentage Discount");
            Console.WriteLine("3. Flat ₹500 Discount");
            Console.WriteLine("4. Festival Discount (25% Off)");
            Console.Write("Choice: ");
            string discountChoice = Console.ReadLine();

            switch (discountChoice)
            {
                case "2": order.DiscountStrategy = new PercentageDiscount(10); break;
                case "3": order.DiscountStrategy = new FlatDiscount(500); break;
                case "4": order.DiscountStrategy = new FestivalDiscount(); break;
            }

            orders.Add(order);
            Console.WriteLine($"\n✅ Order placed successfully! Order ID: {order.OrderId}");
            Console.WriteLine($"Total Amount: ₹{order.GetTotalAmount():F2}");
            if (order.DiscountStrategy != null)
                Console.WriteLine($"Discount Applied: {order.DiscountStrategy.GetDiscountName()}");
        }

        static void ViewOrders()
        {
            Console.WriteLine("\n--- All Orders ---");
            foreach (var order in orders)
            {
                Console.WriteLine($"\nOrder ID: {order.OrderId}");
                Console.WriteLine($"Customer: {order.Customer.Name}");
                Console.WriteLine($"Date: {order.OrderDate:dd-MMM-yyyy}");
                Console.WriteLine($"Status: {order.Status}");
                Console.WriteLine($"Total: ₹{order.GetTotalAmount():F2}");
                Console.WriteLine("Items:");
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"  - {item.Product.Name} x {item.Quantity} = ₹{item.TotalPrice():F2}");
                }
            }
        }

        static void UpdateOrderStatus()
        {
            Console.Write("\nEnter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                Console.WriteLine("❌ Order not found!");
                return;
            }

            Console.WriteLine("\nSelect New Status:");
            Console.WriteLine("1. Pending");
            Console.WriteLine("2. Processing");
            Console.WriteLine("3. Shipped");
            Console.WriteLine("4. Delivered");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            order.Status = choice switch
            {
                "1" => OrderStatus.Pending,
                "2" => OrderStatus.Processing,
                "3" => OrderStatus.Shipped,
                "4" => OrderStatus.Delivered,
                _ => order.Status
            };

            Console.WriteLine($"✅ Order status updated to: {order.Status}");
        }

        static void CancelOrder()
        {
            Console.Write("\nEnter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                Console.WriteLine("❌ Order not found!");
                return;
            }

            order.CancelOrder();
            Console.WriteLine("✅ Order cancelled successfully!");
        }

        static void ShowLinqReports()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║   LINQ REPORTS                         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            // 1. Orders in last 7 days
            Console.WriteLine("\n1. Orders placed in last 7 days:");
            var recentOrders = orders.Where(o => o.OrderDate >= DateTime.Now.AddDays(-7));
            foreach (var order in recentOrders)
            {
                Console.WriteLine($"   Order #{order.OrderId} - {order.Customer.Name} - ₹{order.GetTotalAmount():F2}");
            }

            // 2. Total revenue
            decimal totalRevenue = orders.Where(o => o.Status != OrderStatus.Cancelled).Sum(o => o.GetTotalAmount());
            Console.WriteLine($"\n2. Total Revenue: ₹{totalRevenue:F2}");

            // 3. Most sold product
            var mostSold = orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .SelectMany(o => o.OrderItems)
                .GroupBy(item => item.Product.Name)
                .OrderByDescending(g => g.Sum(item => item.Quantity))
                .FirstOrDefault();
            if (mostSold != null)
                Console.WriteLine($"\n3. Most Sold Product: {mostSold.Key} ({mostSold.Sum(item => item.Quantity)} units)");

            // 4. Top 5 customers by spending
            Console.WriteLine("\n4. Top 5 Customers by Spending:");
            var topCustomers = orders
                .Where(o => o.Status != OrderStatus.Cancelled)
                .GroupBy(o => o.Customer.Name)
                .Select(g => new { Customer = g.Key, TotalSpent = g.Sum(o => o.GetTotalAmount()) })
                .OrderByDescending(x => x.TotalSpent)
                .Take(5);
            foreach (var customer in topCustomers)
            {
                Console.WriteLine($"   {customer.Customer}: ₹{customer.TotalSpent:F2}");
            }

            // 5. Orders grouped by status
            Console.WriteLine("\n5. Orders Grouped by Status:");
            var groupedOrders = orders.GroupBy(o => o.Status);
            foreach (var group in groupedOrders)
            {
                Console.WriteLine($"   {group.Key}: {group.Count()} orders");
            }

            // 6. Products with stock < 10
            Console.WriteLine("\n6. Products with Low Stock (< 10):");
            var lowStock = products.Where(p => p.Stock < 10);
            foreach (var product in lowStock)
            {
                Console.WriteLine($"   {product.Name}: {product.Stock} units");
            }
        }
    }
}
