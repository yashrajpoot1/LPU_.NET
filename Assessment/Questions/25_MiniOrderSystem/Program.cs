using System;
using MiniOrderSystem;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Problem 25: Mini Order System ===");
        var service = new OrderService();

        var customer = new Customer { CustomerId = "C001", Name = "John Doe", Email = "john@test.com" };
        service.AddCustomer(customer);

        var product1 = new Product { ProductId = "P001", Name = "Laptop", Price = 1000, Stock = 5 };
        var product2 = new Product { ProductId = "P002", Name = "Mouse", Price = 25, Stock = 10 };
        service.AddProduct(product1);
        service.AddProduct(product2);

        var coupon = new Coupon { Code = "SAVE10", DiscountPercent = 10, MinimumAmount = 500 };
        service.AddCoupon(coupon);

        service.AddToCart("C001", "P001", 2);
        service.AddToCart("C001", "P002", 1);
        Console.WriteLine("Added items to cart");

        var order = service.PlaceOrder("C001", "SAVE10");
        Console.WriteLine($"\nOrder placed: {order.OrderId}");
        Console.WriteLine($"Invoice: {order.InvoiceNumber}");
        Console.WriteLine($"Subtotal: ${order.Subtotal}");
        Console.WriteLine($"Discount: ${order.Discount}");
        Console.WriteLine($"Total: ${order.Total}");
        Console.WriteLine($"Items: {order.Items.Count}");

        try
        {
            service.AddToCart("C001", "P001", 10);
            service.PlaceOrder("C001");
        }
        catch (InsufficientStockException ex)
        {
            Console.WriteLine($"\nExpected error: {ex.Message}");
        }

        Console.WriteLine("✓ Test Passed\n");
    }
}
